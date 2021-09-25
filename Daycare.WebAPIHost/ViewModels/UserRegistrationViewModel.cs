using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class UserRegistrationViewModel {
        /******************************************************
         * MicrosoftAspNetCore.Identity.IdentityUser
         * ***************************************************/
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        
        /******************************************************
         * Added by OZ
         * ***************************************************/
        public string password { get; set; } //view model only

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string prefix { get; set; }
        public string shimei { get; set; }
        public string myoji { get; set; }
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
        public string telNo { get; set; }
        public string timezoneName { get; set; }
        public int? utcdiff { get; set; }
        public string token { get; set; }
        public string organizationName { get; set; }
        public int? organizationId { get; set; }
        public string userType { get; set; } //Owner, Staff, Parent
        public DateTime? registeredDate { get; set; }
        public DateTime? lastAccessedDate { get; set; }
    }
}
