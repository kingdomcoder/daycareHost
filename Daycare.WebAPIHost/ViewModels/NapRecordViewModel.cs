using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class NapRecordViewModel {
        public int? napRecordId { get; set; }
        public int? childId { get; set; }
        public int? organizationId { get; set; }
        public string parent1Id { get; set; }
        public string parent2Id { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
        public string imagePath { get; set; }
        public string imageFileName { get; set; }
        public DateTime? targetDate { get; set; }

        public string napType { get; set; }
        public string startStampTime { get; set; } // Need to convert to DateTime
        public string endStampTime { get; set; }  // Need to convert to DateTime
        public string description { get; set; }
        public bool? visibleToStaffOnly { get; set; }
        public string createdDate { get; set; } // Need to convert to DateTime
        public string createdBy { get; set; }

        public string firstStartStampTime { get; set; } // Need to convert to DateTime
        public string firstEndStampTime { get; set; } // Need to convert to DateTime
        public string firstDescription { get; set; }
        public bool? firstVisibleToStaffOnly { get; set; }
        public string firstCreatedDate { get; set; } // Need to convert to DateTime
        public string firstCreatedBy { get; set; }

        public string secondStartStampTime { get; set; } // Need to convert to DateTime
        public string secondEndStampTime { get; set; } // Need to convert to DateTime
        public string secondDescription { get; set; }
        public bool? secondVisibleToStaffOnly { get; set; }
        public string secondCreatedDate { get; set; } // Need to convert to DateTime
        public string secondCreatedBy { get; set; }

        public string thirdStartStampTime { get; set; } // Need to convert to DateTime
        public string thirdEndStampTime { get; set; } // Need to convert to DateTime
        public string thirdDescription { get; set; }
        public bool? thirdVisibleToStaffOnly { get; set; }
        public string thirdCreatedDate { get; set; } // Need to convert to DateTime
        public string thirdCreatedBy { get; set; }

        public string updatedDate { get; set; }  // Need to convert to DateTime
        public string updatedBy { get; set; }
    }
}
