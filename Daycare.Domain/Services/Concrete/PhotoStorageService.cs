using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Daycare.Domain.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System;

namespace Daycare.Domain.Services.Concrete {
    public class PhotoStorageService : IPhotoStorageService {
        private readonly string accountName;
        private readonly string accountKey;
        private readonly string containerName;
        private readonly int uploadSasMinutes;
        private readonly int readSasMinutes;

        public PhotoStorageService(IConfiguration configuration) {
            // Secrets are read from configuration (environment variables / user-secrets / Azure app settings).
            // They are NOT hardcoded and NOT shipped to the client.
            accountName = configuration["Azure:Blob:AccountName"];
            accountKey = configuration["Azure:Blob:AccountKey"];
            containerName = configuration["Azure:Blob:PhotoContainer"] ?? "photo";

            uploadSasMinutes = int.TryParse(configuration["Azure:Blob:UploadSasMinutes"], out var up) ? up : 15;
            // Shortened read-SAS lifetime (was 30) to reduce the window a leaked URL is usable.
            readSasMinutes = int.TryParse(configuration["Azure:Blob:ReadSasMinutes"], out var rd) ? rd : 10;
        }

        public PhotoSasResult CreateUploadSas(int organizationId, int childId, string fileName, string contentType) {
            // Server decides the blob path; the client never picks an arbitrary location.
            var extension = string.IsNullOrEmpty(fileName) ? string.Empty : System.IO.Path.GetExtension(fileName);
            var blobName = $"{organizationId}/{childId}/{Guid.NewGuid():N}{extension}";

            // Pin the Content-Type the SAS is valid for. Azure rejects the PUT if the client's
            // x-ms-blob-content-type header does not match the value signed into the SAS, so a
            // non-image upload against this SAS will fail server-side.
            var sas = BuildSas(blobName,
                BlobSasPermissions.Write | BlobSasPermissions.Create,
                uploadSasMinutes,
                contentType);

            return new PhotoSasResult {
                BlobName = blobName,
                UploadUrl = sas
            };
        }

        public ProfileSasResult CreateProfileUploadSas(int childId, string fileName, string contentType) {
            // M2c: profile images (Azure rollback path). Mirrors the S3 implementation's key shape
            // so PhotoController and the client behave identically regardless of the backing store.
            var extension = string.IsNullOrEmpty(fileName) ? string.Empty : System.IO.Path.GetExtension(fileName);
            var imageFileName = $"{childId}/{Guid.NewGuid():N}{extension}";
            var blobName = $"profile/{imageFileName}";

            var sas = BuildSas(blobName,
                BlobSasPermissions.Write | BlobSasPermissions.Create,
                uploadSasMinutes,
                contentType);

            return new ProfileSasResult {
                BlobName = blobName,
                UploadUrl = sas,
                ImageFileName = imageFileName
            };
        }

        public string CreateReadSasUrl(string blobName) {
            return BuildSas(blobName, BlobSasPermissions.Read, readSasMinutes, null);
        }

        private string BuildSas(string blobName, BlobSasPermissions permissions, int validMinutes, string contentType) {
            if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(accountKey)) {
                throw new InvalidOperationException("Azure Blob storage is not configured. Set Azure:Blob:AccountName and Azure:Blob:AccountKey.");
            }

            var credential = new StorageSharedKeyCredential(accountName, accountKey);
            var blobUri = new Uri($"https://{accountName}.blob.core.windows.net/{containerName}/{blobName}");
            var builder = new BlobUriBuilder(blobUri);

            var sasBuilder = new BlobSasBuilder {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(validMinutes)
            };
            sasBuilder.SetPermissions(permissions);
            if (!string.IsNullOrEmpty(contentType)) {
                // Lock the SAS to a single Content-Type (image/*) for upload.
                sasBuilder.ContentType = contentType;
            }

            builder.Sas = sasBuilder.ToSasQueryParameters(credential);
            return builder.ToUri().ToString();
        }
    }
}
