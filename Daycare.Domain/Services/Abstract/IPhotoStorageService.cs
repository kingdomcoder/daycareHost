namespace Daycare.Domain.Services.Abstract {
    public interface IPhotoStorageService {
        // Short-lived SAS granting write/create on a single blob (staff upload).
        // contentType pins the Content-Type the client is allowed to upload.
        PhotoSasResult CreateUploadSas(int organizationId, int childId, string fileName, string contentType);

        // M2c: Short-lived SAS granting write/create on a child's PROFILE image blob.
        // Server decides the key shape "profile/{childId}/{GUID:N}{.ext}". Returns the full
        // object key (BlobName, used for the presigned PUT) and the path to persist on
        // Child.ImageFileName (ImageFileName = "{childId}/{GUID:N}{.ext}", i.e. the key WITHOUT
        // the "profile/" prefix, because the client read base URL already ends with "profile/").
        ProfileSasResult CreateProfileUploadSas(int childId, string fileName, string contentType);

        // Short-lived SAS granting read on a single existing blob (authorized viewer).
        string CreateReadSasUrl(string blobName);
    }

    public class PhotoSasResult {
        public string BlobName { get; set; }
        public string UploadUrl { get; set; }
    }

    public class ProfileSasResult {
        // Full S3 object key the presigned PUT targets, e.g. "profile/12/ab..ef.jpg".
        public string BlobName { get; set; }
        // Presigned PUT URL (Content-Type pinned into the signature).
        public string UploadUrl { get; set; }
        // Value to persist on Child.ImageFileName, e.g. "12/ab..ef.jpg".
        // Read URL = PROFILE_IMAGE_BASE_URL (".../profile/") + ImageFileName.
        public string ImageFileName { get; set; }
    }
}
