using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Concrete;
using Daycare.Domain.Services.Abstract;
using Daycare.WebAPIHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Daycare.WebAPIHost.Controllers {
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class RegistrationController : Controller {

        IRegistrationService registrationService;
        IUserService userService;
        public RegistrationController(IRegistrationService registrationService,
            IUserService userService) {
            this.registrationService = registrationService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("createOrganization")]
        public async Task<IActionResult> createOrganization([FromBody] OrganizationViewModel model) {
            Organization organization = new Organization();
            try {
                // 1) Create new organization
                if (model == null) { return new StatusCodeResult(500); }
                organization.OrganizationId = model.organizationId ?? 0;
                organization.OrganizationName = model.organizationName;
                organization.OrganiationCode = model.organiationCode;
                organization.OrganizationType = model.organizationType;
                organization.OwnerName = model.ownerName;
                organization.ContactEmail = model.contactEmail;
                organization.ContactTelNo = model.contactTelNo;
                organization.Street = model.street;
                organization.Street2 = model.street2;
                organization.City = model.city;
                organization.State = model.state;
                organization.Zip = model.zip;
                organization.Country = model.country;
                organization.Yubin_Bango = model.yubin_Bango;
                organization.To_Do_Fu_Ken = model.to_Do_Fu_Ken;
                organization.Shi_Gun_Ku = model.shi_Gun_Ku;
                organization.Cho_Son = model.cho_Son;
                organization.Apartment_Etc = model.apartment_Etc;
                organization.NumberOfChildGroup = model.numberOfChildGroup;
                organization.RegisteredDate = model.RegisteredDate;
                var newOrganization = registrationService.createOrganization(organization);

                // 2) Update owners profile with organization name and id;
                ApplicationUser user = new ApplicationUser();
                user.Email = model.contactEmail;

                List<ApplicationUser> users = userService.GetUsersBySearchKey(user).ToList();
                var updatingUser = users[0];
                updatingUser.OrganizationName = organization.OrganizationName;
                updatingUser.OrganizationId = organization.OrganizationId;
                userService.UpdateUser(updatingUser);

                return Ok();
            } catch (Exception ex) {
                /************************************************
                 * Added to Monitor user's error action
                 * *********************************************/
                //        userService.SendErrorMessageToAdmin(user, ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
