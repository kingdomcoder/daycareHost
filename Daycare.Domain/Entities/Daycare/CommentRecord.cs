using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class CommentRecord {
        [Key]
        public int CommentRecordId { get; set; }
        public int? ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public bool? Absence { get; set; }
        public bool? Tardy { get; set; }
        public bool? Others { get; set; }
        public string Comment1 { get; set; }
        public string Comment2 { get; set; }
        public string Comment3 { get; set; }
        public string Memo1 { get; set; }
        public string Memo2 { get; set; }
        public string Memo3 { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? RecordedDate { get; set; }
    }
}
