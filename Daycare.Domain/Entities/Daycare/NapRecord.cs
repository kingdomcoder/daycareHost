using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class NapRecord {
        [Key]
        public int NapRecordId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? TargetDate { get; set; }

        public string NapType { get; set; }
        public DateTime? StartStampTime { get; set; }
        public DateTime? EndStampTime { get; set; }
        public string Description { get; set; }
        public bool? VisibleToStaffOnly { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? FirstStartStampTime { get; set; }
        public DateTime? FirstEndStampTime { get; set; }
        public string FirstDescription { get; set; }
        public bool? FirstVisibleToStaffOnly { get; set; }
        public DateTime? FirstCreatedDate { get; set; }
        public string FirstCreatedBy { get; set; }

        public DateTime? SecondStartStampTime { get; set; }
        public DateTime? SecondEndStampTime { get; set; }
        public string SecondDescription { get; set; }
        public bool? SecondVisibleToStaffOnly { get; set; }
        public DateTime? SecondCreatedDate { get; set; }
        public string SecondCreatedBy { get; set; }

        public DateTime? ThirdStartStampTime { get; set; }
        public DateTime? ThirdEndStampTime { get; set; }
        public string ThirdDescription { get; set; }
        public bool? ThirdVisibleToStaffOnly { get; set; }
        public DateTime? ThirdCreatedDate { get; set; }
        public string ThirdCreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
