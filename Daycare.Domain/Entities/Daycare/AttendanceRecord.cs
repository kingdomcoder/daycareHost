using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class AttendanceRecord {
        [Key]
        public int AttendanceRecordId { get; set; }
        public int? ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? RecordedDate { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public bool? Tardy { get; set; }
        public bool? Absent { get; set; }
        public bool? LeaveEarly { get; set; }
        public string Reason { get; set; }
        public string Memo { get; set; }
    }
}
