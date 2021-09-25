using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities.Daycare {
    public class Organization {
        [Key]
        public int  OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string OwnerName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelNo { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Yubin_Bango { get; set; }
        public string To_Do_Fu_Ken { get; set; }
        public string Shi_Gun_Ku { get; set; }
        public string Cho_Son { get; set; }
        public string Apartment_Etc { get; set; }

        public string NumberOfChildGroup { get; set; }
        public string OrganiationCode { get; set; }
        public DateTime? RegisteredDate { get; set; }

        public DateTime? TargetDate { get; set; }
    }
}
