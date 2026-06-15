using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daycare.WebAPIHost.Controllers {
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    [Authorize]
    public class PhotoController : Controller {
        private readonly IPhotoService photoService;
        private readonly IPhotoStorageService photoStorageService;
        private readonly IUserService userService;

        public PhotoController(
            IPhotoService photoService,
            IPhotoStorageService photoStorageService,
            IUserService userService) {
            this.photoService = photoService;
            this.photoStorageService = photoStorageService;
            this.userService = userService;
        }

        // Staff/Owner requests a short-lived write SAS to upload a photo for a child in their organization.
        [HttpPost("createUploadSas")]
        public IActionResult CreateUploadSas([FromBody] PhotoUploadRequestViewModel model) {
            // Input validation -> 400 (do not leak internal details).
            if (model == null || model.ChildId == null) { return BadRequest("ChildId is required"); }
            // Constrain to image content; reject anything that is not an image up front.
            if (!IsAllowedImageContentType(model.ContentType)) {
                return BadRequest("ContentType must be an image/* type");
            }

            try {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return Unauthorized(); }

                var user = userService.GetUserProfileById(userId);
                if (user == null) { return Unauthorized(); }

                // Only staff/owner may upload, and only for children in their own organization.
                if (!IsStaffOrOwner(user) || user.OrganizationId == null) { return Forbid(); }
                if (!photoService.IsChildInOrganization(model.ChildId.Value, user.OrganizationId.Value)) {
                    return Forbid();
                }

                var sas = photoStorageService.CreateUploadSas(
                    user.OrganizationId.Value, model.ChildId.Value, model.FileName, model.ContentType);

                return Json(sas, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception) {
                // Server failure -> 500 without exposing internal details.
                return StatusCode(500);
            }
        }

        // M2c: Staff/Owner requests a short-lived write SAS to upload a child's PROFILE image.
        // Mirrors createUploadSas (same authorization), but the key shape is "profile/{childId}/{GUID:N}{.ext}".
        // The returned ImageFileName is what the client persists via /api/child/saveProfileFilePath
        // (Child.ImageFileName); the read URL is PROFILE_IMAGE_BASE_URL + ImageFileName.
        [HttpPost("createProfileUploadSas")]
        public IActionResult CreateProfileUploadSas([FromBody] PhotoUploadRequestViewModel model) {
            if (model == null || model.ChildId == null) { return BadRequest("ChildId is required"); }
            if (!IsAllowedImageContentType(model.ContentType)) {
                return BadRequest("ContentType must be an image/* type");
            }

            try {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return Unauthorized(); }

                var user = userService.GetUserProfileById(userId);
                if (user == null) { return Unauthorized(); }

                // Only staff/owner may upload, and only for children in their own organization.
                if (!IsStaffOrOwner(user) || user.OrganizationId == null) { return Forbid(); }
                if (!photoService.IsChildInOrganization(model.ChildId.Value, user.OrganizationId.Value)) {
                    return Forbid();
                }

                var sas = photoStorageService.CreateProfileUploadSas(
                    model.ChildId.Value, model.FileName, model.ContentType);

                return Json(sas, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception) {
                return StatusCode(500);
            }
        }

        // Staff/Owner persists photo metadata after the blob has been uploaded via the SAS above.
        [HttpPost("savePhoto")]
        public IActionResult SavePhoto([FromBody] PhotoMetaViewModel model) {
            // Input validation -> 400 (do not leak internal details).
            if (model == null || model.ChildId == null || string.IsNullOrEmpty(model.BlobName)) {
                return BadRequest("ChildId and BlobName are required");
            }
            if (!IsAllowedImageContentType(model.ContentType)) {
                return BadRequest("ContentType must be an image/* type");
            }
            if (model.Caption != null && model.Caption.Length > MaxCaptionLength) {
                return BadRequest("Caption is too long");
            }

            try {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return Unauthorized(); }

                var user = userService.GetUserProfileById(userId);
                if (user == null) { return Unauthorized(); }

                if (!IsStaffOrOwner(user) || user.OrganizationId == null) { return Forbid(); }
                if (!photoService.IsChildInOrganization(model.ChildId.Value, user.OrganizationId.Value)) {
                    return Forbid();
                }

                // ❶ Authorization-bypass / path-traversal fix: the client must not be able to
                // register an arbitrary BlobName (e.g. another org/child path, or a "../" escape)
                // and thereby obtain a read SAS for someone else's blob. The blob path is
                // server-decided in CreateUploadSas as
                // "{organizationId}/{childId}/{Guid:N}{extension}" (see PhotoStorageService).
                //
                // (1) Strict structural validation. A prefix check alone is insufficient because
                //     StartsWith does not normalize "..", so a value like
                //     "5/10/../../6/11/<guid>.jpg" would pass the prefix yet resolve elsewhere.
                //     This regex enforces the exact "{org}/{child}/{32-hex-GUID}{.ext}" shape and
                //     rejects "..", extra/empty segments, and leading/duplicate slashes.
                if (!BlobNameFormat.IsMatch(model.BlobName)) {
                    return BadRequest("Invalid BlobName");
                }
                // (2) Ownership prefix check (retained): the org/child segments must belong to the
                //     calling user. Both (1) and (2) must hold.
                var requiredPrefix = $"{user.OrganizationId.Value}/{model.ChildId.Value}/";
                if (!model.BlobName.StartsWith(requiredPrefix, StringComparison.Ordinal)) {
                    return Forbid();
                }

                var photo = new Photo {
                    ChildId = model.ChildId,
                    OrganizationId = user.OrganizationId,
                    BlobName = model.BlobName,
                    FileName = model.FileName,
                    ContentType = model.ContentType,
                    Caption = model.Caption,
                    UploadedBy = userId,
                };
                var saved = photoService.SavePhoto(photo);

                return Json(saved, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception) {
                return StatusCode(500);
            }
        }

        // Returns photos of a child. A parent may only view their own child; staff/owner only within their org.
        // Each returned photo includes a short-lived read SAS URL (the container is private).
        [HttpGet("getPhotosByChild/{childId}")]
        public IActionResult GetPhotosByChild(int childId) {
            try {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return Unauthorized(); }

                var user = userService.GetUserProfileById(userId);
                if (user == null) { return Unauthorized(); }

                // ❷ Whitelist authorization (no permissive fallback). A user is granted access
                // ONLY when explicitly identified as staff/owner of the child's org, OR explicitly
                // identified as a registered parent of the child. Any other case (unknown UserType,
                // null OrganizationId, etc.) is denied. Mirrors createUploadSas/savePhoto.
                bool authorized = false;
                if (IsStaffOrOwner(user) && user.OrganizationId != null) {
                    authorized = photoService.IsChildInOrganization(childId, user.OrganizationId.Value);
                } else if (IsParent(user)) {
                    authorized = photoService.IsParentOfChild(userId, childId);
                }
                if (!authorized) { return Forbid(); }

                var photos = photoService.GetPhotosByChild(childId);
                var result = photos.Select(p => new PhotoViewModel {
                    PhotoId = p.PhotoId,
                    ChildId = p.ChildId,
                    OrganizationId = p.OrganizationId,
                    FileName = p.FileName,
                    ContentType = p.ContentType,
                    Caption = p.Caption,
                    CreatedDate = p.CreatedDate,
                    Url = photoStorageService.CreateReadSasUrl(p.BlobName),
                }).ToList();

                return Json(result, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception) {
                return StatusCode(500);
            }
        }

        // ❺ Soft-delete (logical delete) for GDPR/personal-data removal requests.
        // Staff/Owner only, and only for photos belonging to their own organization.
        [HttpPost("deletePhoto")]
        public IActionResult DeletePhoto([FromBody] PhotoDeleteViewModel model) {
            if (model == null || model.PhotoId == null) { return BadRequest("PhotoId is required"); }

            try {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) { return Unauthorized(); }

                var user = userService.GetUserProfileById(userId);
                if (user == null) { return Unauthorized(); }

                // Whitelist: only staff/owner with a known organization may delete.
                if (!IsStaffOrOwner(user) || user.OrganizationId == null) { return Forbid(); }

                var photo = photoService.GetPhotoById(model.PhotoId.Value);
                if (photo == null) { return NotFound(); }

                // Same-org check: cannot delete another organization's photo.
                if (photo.OrganizationId == null || photo.OrganizationId.Value != user.OrganizationId.Value) {
                    return Forbid();
                }

                photoService.SoftDeletePhoto(model.PhotoId.Value);
                return Ok();
            } catch (Exception) {
                return StatusCode(500);
            }
        }

        // M3 (Task 2, Option A): serve a child's PROFILE image while the S3 bucket stays FULLY PRIVATE.
        //
        // Why this endpoint exists: the bucket has all public access blocked (no public policy), so the
        // old "PROFILE_IMAGE_BASE_URL + imageFileName" direct-S3 read returns 403. Child gallery photos
        // already solve this with server-issued presigned GET URLs (getPhotosByChild). Profile images are
        // displayed by plain Image.network in ~13 places that cannot attach an Authorization header, so we
        // expose an ANONYMOUS endpoint that issues a short-lived presigned GET and 302-redirects to it.
        //   Front-end PROFILE_IMAGE_BASE_URL = <API CloudFront>/api/photo/profile/   (trailing slash).
        //   Request URL = base + Child.ImageFileName ("{childId}/{GUID:N}{.ext}").
        //
        // Privacy guarantee for CHILD PHOTOS (must remain private):
        //   - The presigned key is hard-coded to the "profile/" prefix: key = "profile/" + imageFileName.
        //     Child gallery photos live under "{org}/{child}/..." (NO "profile/" prefix), so this endpoint
        //     can NEVER mint a read URL for a gallery photo.
        //   - imageFileName is validated to the exact server-decided shape "{childId}/{32-hex GUID}{.ext}"
        //     (see S3PhotoStorageService.CreateProfileUploadSas), rejecting "..", extra segments, and any
        //     attempt to break out of the profile/ prefix.
        //   - Anonymous read is behavior-compatible with the previous Azure deployment, where profile
        //     image URLs were public. Profile keys are random GUIDs (not enumerable) and the bucket stays
        //     non-public/non-listable.
        [AllowAnonymous]
        [HttpGet("profile/{childId:int}/{fileName}")]
        public IActionResult GetProfileImage(int childId, string fileName) {
            // Reconstruct the imageFileName the client appended to the base URL and validate strictly.
            var imageFileName = $"{childId}/{fileName}";
            if (!ProfileImageFileNameFormat.IsMatch(imageFileName)) {
                return BadRequest("Invalid profile image path");
            }

            try {
                // Hard-pin the "profile/" prefix; this is the ONLY prefix this endpoint can ever read.
                var blobName = $"profile/{imageFileName}";
                var url = photoStorageService.CreateReadSasUrl(blobName);
                // 302 to the short-lived presigned GET. The browser follows it; the bucket stays private.
                return Redirect(url);
            } catch (Exception) {
                return StatusCode(500);
            }
        }

        // Profile key shape (without the "profile/" prefix): "{childId}/{GUID:N}{.ext}".
        private static readonly Regex ProfileImageFileNameFormat =
            new Regex(@"^\d+/[0-9a-fA-F]{32}(\.[A-Za-z0-9]+)?$", RegexOptions.Compiled);

        private string GetUserId() {
            // The JWT "sub" claim carries ApplicationUser.Id (see TokenController).
            return User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private bool IsStaffOrOwner(Daycare.Domain.Entities.ApplicationUser user) {
            var type = user.UserType ?? string.Empty;
            return type.Equals("Staff", StringComparison.OrdinalIgnoreCase)
                || type.Equals("Owner", StringComparison.OrdinalIgnoreCase)
                || type.Equals("Organizer", StringComparison.OrdinalIgnoreCase);
        }

        // Explicit (whitelist) parent check, symmetric to IsStaffOrOwner.
        private bool IsParent(Daycare.Domain.Entities.ApplicationUser user) {
            var type = user.UserType ?? string.Empty;
            return type.Equals("Parent", StringComparison.OrdinalIgnoreCase);
        }

        // ❹ Restrict uploads/metadata to image content only.
        // Strict whitelist: a permissive "image/" prefix would also admit "image/svg+xml",
        // which is an active XSS vector (SVG can embed scripts). Only raster image types are
        // accepted here.
        private static bool IsAllowedImageContentType(string contentType) {
            if (string.IsNullOrWhiteSpace(contentType)) { return false; }
            return AllowedImageContentTypes.Contains(contentType.Trim());
        }

        private static readonly HashSet<string> AllowedImageContentTypes =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
                "image/jpeg",
                "image/png",
                "image/gif",
                "image/webp",
            };

        // Server-decided blob path shape: "{org}/{child}/{GUID:N}{.ext}" (see PhotoStorageService).
        // GUID:N => 32 lowercase hex chars; extension via Path.GetExtension includes the leading dot.
        private static readonly Regex BlobNameFormat =
            new Regex(@"^\d+/\d+/[0-9a-fA-F]{32}(\.[A-Za-z0-9]+)?$", RegexOptions.Compiled);

        private const int MaxCaptionLength = 1000;
    }

    public class PhotoUploadRequestViewModel {
        public int? ChildId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }

    public class PhotoMetaViewModel {
        public int? ChildId { get; set; }
        public string BlobName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Caption { get; set; }
    }

    public class PhotoDeleteViewModel {
        public int? PhotoId { get; set; }
    }

    public class PhotoViewModel {
        public int PhotoId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Caption { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Url { get; set; }
    }
}
