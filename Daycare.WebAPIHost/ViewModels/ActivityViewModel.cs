using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class ActivityViewModel {
        public int? activityId { get; set; }
        public int? childId { get; set; }
        public string parent1Id { get; set; }
        public string parent2Id { get; set; }
        public int? organizationId { get; set; }
        public DateTime? targetDate { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
        public string childMiddleName { get; set; }
        public string childShimei { get; set; }
        public string childMyoji { get; set; }
        public string activityName { get; set; }
        public string activityDescription { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
