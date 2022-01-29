using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class PottyRecord {
        [Key]
        public int PottyRecordId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? TargetDate { get; set; }

        public string PottyType { get; set; }
        public DateTime? StampTime { get; set; }
        public string Description { get; set; }
        public bool? VisibleToStaffOnly { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? FirstStampTime { get; set; }
        public string FirstDescription { get; set; }
        public bool? FirstVisibleToStaffOnly { get; set; }
        public DateTime? FirstCreatedDate { get; set; }
        public string FirstCreatedBy { get; set; }

        public DateTime? SecondStampTime { get; set; }
        public string SecondDescription { get; set; }
        public bool? SecondVisibleToStaffOnly { get; set; }
        public DateTime? SecondCreatedDate { get; set; }
        public string SecondCreatedBy { get; set; }

        public DateTime? ThirdStampTime { get; set; }
        public string ThirdDescription { get; set; }
        public bool? ThirdVisibleToStaffOnly { get; set; }
        public DateTime? ThirdCreatedDate { get; set; }
        public string ThirdCreatedBy { get; set; }

        public DateTime? ForthStampTime { get; set; }
        public string ForthDescription { get; set; }
        public bool? ForthVisibleToStaffOnly { get; set; }
        public DateTime? ForthCreatedDate { get; set; }
        public string ForthCreatedBy { get; set; }

        public DateTime? FifthStampTime { get; set; }
        public string FifthDescription { get; set; }
        public bool? FifthVisibleToStaffOnly { get; set; }
        public DateTime? FifthCreatedDate { get; set; }
        public string FifthCreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
