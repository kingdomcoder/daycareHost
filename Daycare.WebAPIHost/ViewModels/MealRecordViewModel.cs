using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class MealRecordViewModel {
        /**********************************************
         * datetime colum must be string, otherwise can not get data from client.
         * However target date can  be DateTime, beecaues it is Date only.
         * *******************************************/
        public int? mealRecordId { get; set; }
        public int? childId { get; set; } 
        public int? organizationId { get; set; }
        public string parent1Id { get; set; }
        public string parent2Id { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
        public string imagePath { get; set; }
        public string imageFileName { get; set; }
        public DateTime? targetDate { get; set; } // DateTime is Ok if it does not include time
        public string endStampTime { get; set; }  // Need to convert to DateTime
        public string mealType { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public bool?  visibleToStaffOnly { get; set; }
        public string createdDate { get; set; } // Need to convert to DateTime
        public string createdBy { get; set; }

        public bool? breakfast { get; set; }
        public string breakfastQuantity { get; set; }
        public string breakfastDescription { get; set; }
        public bool? breakfastVisibleToStaffOnly { get; set; }
        public string breakfastCreatedDate { get; set; } // Need to convert to DateTime
        public string breakfastCreatedBy { get; set; }

        public bool? amSnack { get; set; }
        public string amSnackQuantity { get; set; }
        public string amSnackDescription { get; set; }
        public bool?  amSnackVisibleToStaffOnly { get; set; }
        public string amSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string amSnackCreatedBy { get; set; }

        public bool? lunch { get; set; }
        public string lunchQuantity { get; set; }
        public string lunchDescription { get; set; }
        public bool?  lunchVisibleToStaffOnly { get; set; }
        public string lunchCreatedDate { get; set; } // Need to convert to DateTime
        public string lunchCreatedBy { get; set; }

        public bool? pmSnack { get; set; }
        public string pmSnackQuantity { get; set; }
        public string pmSnackDescription { get; set; }
        public bool? pmSnackVisibleToStaffOnly { get; set; }
        public string pmSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string pmSnackCreatedBy { get; set; }

        public bool? lateSnack { get; set; }
        public string lateSnackQuantity { get; set; }
        public string lateSnackDescription { get; set; }
        public bool? lateSnackVisibleToStaffOnly { get; set; }
        public string lateSnackCreatedDate { get; set; } // Need to convert to DateTime
        public string lateSnackCreatedBy { get; set; }

        public bool? dinner { get; set; }
        public string dinnerQuantity { get; set; }
        public string dinnerDescription { get; set; }
        public bool?  dinnerVisibleToStaffOnly { get; set; }
        public string dinnerCreatedDate { get; set; } // Need to convert to DateTime
        public string dinnerCreatedBy { get; set; }

        public string updatedDate { get; set; }  // Need to convert to DateTime
        public string updatedBy { get; set; }
    }
}
