using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class DaycareUserViewModel {
        public string DaycareUserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Shimei { get; set; }
        public string Myoji { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string TelNo { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string OrganizationName { get; set; }
        public int? OrganizationId { get; set; }
        public string UserType { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? LastAccessedDate { get; set; }
    }
}
