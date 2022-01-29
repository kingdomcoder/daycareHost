using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class Activity {
        [Key]
        public int ActivityId { get; set; }
        public int? ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public DateTime? TargetDate { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ChildMiddleName { get; set; }
        public string ChildShimei { get; set; }
        public string ChildMyoji { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }


    public class Photo {
        [Key]
        public int PhotoId { get; set; }
        public int? ActivityId { get; set; }
        public string PhotoURL { get; set; }
        public string Memo { get; set; }
        }


    public class MealRecord {
        [Key]
        public int MealRecordId { get; set; }
        public int? ActivityId { get; set; }
        public string MealType { get; set; }
        public string ItemName { get; set; }
        public bool? Completed { get; set; }
        }


    }
}
