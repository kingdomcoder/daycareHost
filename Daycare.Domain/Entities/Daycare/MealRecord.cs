using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class MealRecord {
        [Key]
        public int? MealRecordId { get; set; }
        public int? ChildId { get; set; }
        public int? OrganizationId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? EndStampTime { get; set; }
        public string MealType { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public bool? VisibleToStaffOnly { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public bool? Breakfast { get; set; }
        public string BreakfastQuantity { get; set; }
        public string BreakfastDescription { get; set; }
        public bool?  BreakfastVisibleToStaffOnly { get; set; }
        public DateTime? BreakfastCreatedDate { get; set; } // Need to convert to DateTime
        public string BreakfastCreatedBy { get; set; }

        public bool? AMSnack { get; set; }
        public string AMSnackQuantity { get; set; }
        public string AMSnackDescription { get; set; }
        public bool?  AMSnackVisibleToStaffOnly { get; set; }
        public DateTime? AMSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string AMSnackCreatedBy { get; set; }

        public bool? Lunch { get; set; }
        public string LunchQuantity { get; set; }
        public string LunchDescription { get; set; }
        public bool?  LunchVisibleToStaffOnly { get; set; }
        public DateTime? LunchCreatedDate { get; set; } // Need to convert to DateTime
        public string LunchCreatedBy { get; set; }

        public bool? PMSnack { get; set; }
        public string PMSnackQuantity { get; set; }
        public string PMSnackDescription { get; set; }
        public bool?  PMSnackVisibleToStaffOnly { get; set; }
        public DateTime? PMSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string PMSnackCreatedBy { get; set; }

        public bool? LateSnack { get; set; }
        public string LateSnackQuantity { get; set; }
        public string LateSnackDescription { get; set; }
        public bool?  LateSnackVisibleToStaffOnly { get; set; }
        public DateTime? LateSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string LateSnackCreatedBy { get; set; }

        public bool? Dinner { get; set; }
        public string DinnerQuantity { get; set; }
        public string DinnerDescription { get; set; }
        public bool?  DinnerVisibleToStaffOnly { get; set; }
        public DateTime? DinnerCreatedDate { get; set; } // Need to convert to DateTime
        public string DinnerCreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
