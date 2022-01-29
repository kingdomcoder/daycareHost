using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class PottyRecordViewModel {
        public int? pottyRecordId { get; set; }
        public int? childId { get; set; }
        public int? organizationId { get; set; }
        public string parent1Id { get; set; }
        public string parent2Id { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
        public string imagePath { get; set; }
        public string imageFileName { get; set; }
        public DateTime? targetDate { get; set; }

        public string pottyType { get; set; }
        public string stampTime { get; set; } // Need to convert to DateTime
        public string description { get; set; }
        public bool? visibleToStaffOnly { get; set; }
        public string createdDate { get; set; } // Need to convert to DateTime
        public string createdBy { get; set; }

        public string firstStampTime { get; set; }  // Need to convert to DateTime
        public string firstDescription { get; set; }
        public bool? firstVisibleToStaffOnly { get; set; }
        public string firstCreatedDate { get; set; } // Need to convert to DateTime
        public string firstCreatedBy { get; set; }

        public string secondStampTime { get; set; } // Need to convert to DateTime
        public string secondDescription { get; set; }
        public bool? secondVisibleToStaffOnly { get; set; }
        public string secondCreatedDate { get; set; } // Need to convert to DateTime
        public string secondCreatedBy { get; set; }

        public string thirdStampTime { get; set; } // Need to convert to DateTime
        public string thirdDescription { get; set; }
        public bool? thirdVisibleToStaffOnly { get; set; }
        public string thirdCreatedDate { get; set; } // Need to convert to DateTime
        public string thirdCreatedBy { get; set; }

        public string forthStampTime { get; set; } // Need to convert to DateTime
        public string forthDescription { get; set; }
        public bool? forthVisibleToStaffOnly { get; set; }
        public string forthCreatedDate { get; set; } // Need to convert to DateTime
        public string forthCreatedBy { get; set; }

        public string fifthStampTime { get; set; } // Need to convert to DateTime
        public string fifthDescription { get; set; }
        public bool? fifthVisibleToStaffOnly { get; set; }
        public string fifthCreatedDate { get; set; } // Need to convert to DateTime
        public string fifthCreatedBy { get; set; }

        public string updatedDate { get; set; } // Need to convert to DateTime
        public string updatedBy { get; set; }
    }
}
