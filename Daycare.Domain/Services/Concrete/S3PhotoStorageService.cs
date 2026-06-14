using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Daycare.Domain.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System;

namespace Daycare.Domain.Services.Concrete {
    // M2: AWS S3 implementation of IPhotoStorageService.
    // 1:1 replacement for the Azure PhotoStorageService:
    //   - SAS write URL          -> S3 presigned PUT URL
    //   - SAS read URL           -> S3 presigned GET URL
    // The server still decides the object key (blobName); the path shape
    // "{org}/{child}/{GUID:N}{.ext}" is preserved so PhotoController's
    // regex validation/authorization keeps working unchanged.
    //
    // Configuration keys:
    //   Aws:S3:BucketName        (required) target bucket
    //   Aws:S3:Region            (e.g. "ap-northeast-1"); ignored if ServiceUrl is set
    //   Aws:S3:ServiceUrl        (optional) e.g. "http://localhost:4566" for LocalStack
    //   Aws:S3:ForcePathStyle    (optional, default true when ServiceUrl is set)
    //   Aws:S3:AccessKey / SecretKey (optional) explicit creds; otherwise the
    //                            standard AWS credential chain (env vars, profile, IAM role) is used.
    //   Aws:S3:UploadSasMinutes / ReadSasMinutes  presigned URL lifetimes.
    public class S3PhotoStorageService : IPhotoStorageService {
        private readonly string bucketName;
        private readonly int uploadSasMinutes;
        private readonly int readSasMinutes;
        private readonly IAmazonS3 s3Client;

        public S3PhotoStorageService(IConfiguration configuration) {
            bucketName = configuration["Aws:S3:BucketName"];

            uploadSasMinutes = int.TryParse(configuration["Aws:S3:UploadSasMinutes"], out var up) ? up : 15;
            // Mirror the Azure service: shortened read lifetime to limit a leaked URL's window.
            readSasMinutes = int.TryParse(configuration["Aws:S3:ReadSasMinutes"], out var rd) ? rd : 10;

            var serviceUrl = configuration["Aws:S3:ServiceUrl"];
            var region = configuration["Aws:S3:Region"];

            var s3Config = new AmazonS3Config();
            if (!string.IsNullOrEmpty(serviceUrl)) {
                // LocalStack / custom endpoint. Path-style addressing is required there
                // (virtual-host style "{bucket}.localhost" does not resolve locally).
                s3Config.ServiceURL = serviceUrl;
                var forcePathStyle = !bool.TryParse(configuration["Aws:S3:ForcePathStyle"], out var fps) || fps;
                s3Config.ForcePathStyle = forcePathStyle;
                if (!string.IsNullOrEmpty(region)) {
                    s3Config.AuthenticationRegion = region;
                }
            } else if (!string.IsNullOrEmpty(region)) {
                s3Config.RegionEndpoint = RegionEndpoint.GetBySystemName(region);
            }

            var accessKey = configuration["Aws:S3:AccessKey"];
            var secretKey = configuration["Aws:S3:SecretKey"];
            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey)) {
                var creds = new BasicAWSCredentials(accessKey, secretKey);
                s3Client = new AmazonS3Client(creds, s3Config);
            } else {
                // Standard AWS credential chain (env vars, shared profile, IAM role).
                s3Client = new AmazonS3Client(s3Config);
            }
        }

        public PhotoSasResult CreateUploadSas(int organizationId, int childId, string fileName, string contentType) {
            // Server decides the object key; the client never picks an arbitrary location.
            // Path shape is identical to the Azure implementation so PhotoController's regex still matches.
            var extension = string.IsNullOrEmpty(fileName) ? string.Empty : System.IO.Path.GetExtension(fileName);
            var blobName = $"{organizationId}/{childId}/{Guid.NewGuid():N}{extension}";

            var url = BuildPresignedUrl(blobName, HttpVerb.PUT, uploadSasMinutes, contentType);

            return new PhotoSasResult {
                BlobName = blobName,
                UploadUrl = url
            };
        }

        public string CreateReadSasUrl(string blobName) {
            return BuildPresignedUrl(blobName, HttpVerb.GET, readSasMinutes, null);
        }

        private string BuildPresignedUrl(string blobName, HttpVerb verb, int validMinutes, string contentType) {
            if (string.IsNullOrEmpty(bucketName)) {
                throw new InvalidOperationException("AWS S3 is not configured. Set Aws:S3:BucketName (and Region or ServiceUrl).");
            }

            var request = new GetPreSignedUrlRequest {
                BucketName = bucketName,
                Key = blobName,
                Verb = verb,
                Expires = DateTime.UtcNow.AddMinutes(validMinutes)
            };

            if (!string.IsNullOrEmpty(contentType)) {
                // Pin the Content-Type into the signature for upload (PUT). S3 requires the client's
                // Content-Type header to match the signed value, so a non-image upload against this
                // presigned URL fails server-side -- equivalent to the Azure SAS ContentType pinning.
                request.ContentType = contentType;
            }

            return s3Client.GetPreSignedURL(request);
        }
    }
}
