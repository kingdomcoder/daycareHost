using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class GrowthRecord {
        [Key]
        public int GrowthRecordId { get; set; }
        public int? ChildId { get; set; }
        public string Parent1Id { get; set; }
        public string Parent2Id { get; set; }
        public int? OrganizationId { get; set; }
        public string ChildFirstName { get; set; }
        public string ChildLastName { get; set; }
        public double? HeightCM { get; set; }
        public double? WeightKG { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}
