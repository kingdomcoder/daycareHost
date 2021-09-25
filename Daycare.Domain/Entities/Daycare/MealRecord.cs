using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class MealRecord {
        [Key]
        public int MealRecordId { get; set; }
        public int? ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string MealType { get; set; }
        public string ItemName { get; set; }
        public bool? Completed { get; set; }

        public string Memo { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? RecordedDate { get; set; }
    }
}
