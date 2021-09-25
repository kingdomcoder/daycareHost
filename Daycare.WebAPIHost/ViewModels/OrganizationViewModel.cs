using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class OrganizationViewModel {
        public int? organizationId { get; set; }
        public string organizationName { get; set; }
        public string organiationCode { get; set; }
        public string organizationType { get; set; }
        public string ownerName { get; set; }
        public string contactEmail { get; set; }
        public string contactTelNo { get; set; }
        public string street { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string yubin_Bango { get; set; }
        public string to_Do_Fu_Ken { get; set; }
        public string shi_Gun_Ku { get; set; }
        public string cho_Son { get; set; }
        public string apartment_Etc { get; set; }

        public string numberOfChildGroup { get; set; }
        public DateTime? RegisteredDate { get; set; }
    }
}
