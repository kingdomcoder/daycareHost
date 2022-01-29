using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class Child {
        [Key]
        public int ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ChildMiddleName { get; set; }
        public string ChildShimei { get; set; }
        public string ChildMyoji { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public int? Grade { get; set; }
        public string ClassName { get; set; }
        public bool? AttendMon { get; set; }
        public bool? AttendTue { get; set; }
        public bool? AttendWed { get; set; }
        public bool? AttendThu { get; set; }
        public bool? AttendFri { get; set; }
        public bool? AttendSat { get; set; }
        public bool? AttendSun { get; set; }
        public DateTime? RegisteredDate { get;set; }

        public DateTime? TargetDate { get; set; }
        public bool? ActiveStatus { get; set; }
    }
}
