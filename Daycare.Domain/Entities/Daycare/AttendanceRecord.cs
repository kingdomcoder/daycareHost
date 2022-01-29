using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class AttendanceRecord {
        [Key]
        public int AttendanceRecordId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? InTime_StampTime { get; set; }
        public string InTime_EnteredBy { get; set; }


        public DateTime? OutTime { get; set; }
        public DateTime? OutTime_StampTime { get; set; }
        public string OutTime_EnteredBy { get; set; }


        public bool? Tardy { get; set; }
        public string TardyComment { get; set; }
        public DateTime? Tardy_StampTime { get; set; }
        public string Tardy_EnteredBy { get; set; }
        public bool? Absent { get; set; }
        public string AbsentComment { get; set; }
        public DateTime? Absent_StampTime { get; set; }
        public string Absent_EnteredBy { get; set; }
        public bool? LeaveEarly { get; set; }
        public string LeaveEarlyComment { get; set; }
        public DateTime? LeaveEarly_StampTime { get; set; }
        public string LeaveEarly_EnteredBy { get; set; }

        public DateTime? CancelInTime_StampTime { get; set; }
        public string CancelInTime_EnteredBy { get; set; }
        public DateTime? CancelOutTime_StampTime { get; set; }
        public string CancelOutTime_EnteredBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        //public int AttendanceRecordId { get; set; }
        //public int? ChildId { get; set; }
        //public string Parent1Id { get; set; }
        //public string Parent2Id { get; set; }
        //public int? OrganizationId { get; set; }
        //public string ChildFirstName { get; set; }
        //public string ChildLastName { get; set; }
        //public DateTime? TargetDate { get; set; }
        //public DateTime? RecordedDate { get; set; }
        //public DateTime? InTime { get; set; }
        //public DateTime? OutTime { get; set; }
        //public bool? Tardy { get; set; }
        //public bool? Absent { get; set; }
        //public bool? LeaveEarly { get; set; }
        //public string Reason { get; set; }
        //public string Memo { get; set; }
        //public string ImagePath { get; set; }
        //public string TardyComment { get; set; }
        //public string AbsentComment { get; set; }
        //public string LeaveEarlyComment { get; set; }

    }
}
