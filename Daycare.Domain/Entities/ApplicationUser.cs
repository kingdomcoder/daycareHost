using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Daycare.Domain.Entities {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {
        }
        //[Key]
        //[Required]
        //public string Id { get; set; } ....... MicrosoftAspNetCore.Identity.IdentityUser

        //[Required]
        //[MaxLength(128)]
        //public string UserName { get; set; }....... MicrosoftAspNetCore.Identity.IdentityUser

        //[Required]
        //public string Email { get; set; }....... MicrosoftAspNetCore.Identity.IdentityUser

        //activated 06/28/21 to distinguish siteground user   
        public override string PasswordHash { get; set; }

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #region Lazy-Load Properties
        /// <summary>
        /// A list of all the quiz created by this users.
        /// </summary>
 //       public virtual List<Quiz> Quizzes { get; set; }

        /// <summary>
        /// A list of all the refresh tokens issued for this users.
        /// </summary>
        //public virtual List<Token> Tokens { get; set; }
        #endregion

        /******************************************************
         * Added by OZ
         * ***************************************************/
        public string FirstName { get; set; } //required
        public string LastName { get; set; } //required
        public string Prefix { get; set; }
        public string Shimei { get; set; }
        public string Myoji { get; set; }
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
        public string TelNo { get; set; }
        public string TimezoneName { get; set; }
        public int? UTCdiff { get; set; }
        public string Token { get; set; }
        public string OrganizationName { get; set; } //required
        public int? OrganizationId { get; set; }     //required
        public string UserType { get; set; } //Organizer, Staff, Parent
        public DateTime? RegisteredDate { get; set; }
        public DateTime? LastAccessedDate { get; set; }
    }
}
