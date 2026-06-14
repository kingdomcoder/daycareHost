using System;
using System.ComponentModel.DataAnnotations;

namespace Daycare.Domain.Entities.Daycare {
    public class Photo {
        [Key]
        public int PhotoId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string BlobName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Caption { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? ActiveStatus { get; set; }
    }
}
