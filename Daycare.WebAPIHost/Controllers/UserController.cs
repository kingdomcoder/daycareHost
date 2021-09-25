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
    public class UserController : BaseApiController {
        private readonly UserManager<ApplicationUser> _userManager;
        private IRegistrationService registrationService;

        IUserService userService;
        #region Constructor
        public UserController(
            MyUserDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IUserService userService,
            IRegistrationService registrationService
            )
            : base(context, roleManager, userManager, configuration) {
            this.userService = userService;
            this._userManager = userManager;
            this.registrationService = registrationService;
        }
        #endregion

        [AllowAnonymous]
        [HttpGet("emailCheck/{email}")]
        public async Task<IActionResult> EmailCheck(string email) {
            //     try {
            // check if there's an user with the given username
            var user = await UserManager.FindByNameAsync(email);
            // fallback to support e-mail address instead of username
            if (user == null && email.Contains("@")) {
                user = await UserManager.FindByEmailAsync(email);
            }
            return Ok(user);
        }

        [HttpGet("getUserProfile/{userName}")]
        public async Task<IActionResult> GetUserProfile(string userName) {
            //     try {
            // check if there's an user with the given username
            var user = await UserManager.FindByNameAsync(userName);
            // fallback to support e-mail address instead of username
            if (user == null && userName.Contains("@")) {
                user = await UserManager.FindByEmailAsync(userName);
            }
            return Json(user, new JsonSerializerOptions {
                WriteIndented = true,
            });
        }

        [HttpGet("getUserProfileById/{id}")]
        public async Task<IActionResult> GetUserProfileById(string id) {
            try {
                if (id == null) { return new StatusCodeResult(500); }
                var user = userService.GetUserProfileById(id);
                return Json(user, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "GetUserProfileById failed");
            }
        }

        [AllowAnonymous]
        [HttpPost("createOwner")]
        public async Task<IActionResult> CreateOwner([FromBody] DaycareUserViewModel model) {
            if (model == null) { return new StatusCodeResult(500); }
            ApplicationUser user = new ApplicationUser();
            try {
                if (model == null) { return new StatusCodeResult(500); }
                if (model.Email.Contains("kelev.biz")) {
                    return new StatusCodeResult(500);
                }
                user.Email = model.Email;
                user.UserName = model.Email;
                user.CreatedDate = DateTime.Today;
                user.LastModifiedDate = DateTime.Today;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Prefix = model.Prefix;
                user.Shimei = model.Shimei;
                user.Myoji = model.Myoji;
                user.Street = model.Street;
                user.Street2 = model.Street2;
                user.City = model.City;
                user.State = model.State;
                user.Zip = model.Zip;
                user.TelNo = model.TelNo;
                user.Token = model.Token;
                user.UserType = model.UserType;
                user.RegisteredDate = DateTime.Now;
                user.LastAccessedDate = DateTime.Now;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    var newUser = userService.GetUsersBySearchKey(user).FirstOrDefault();
             //       return Ok(newUser);


                    return Json(newUser, new JsonSerializerOptions {
                        WriteIndented = true,
                    });


                } else {
                    //return BadRequest(result.Errors);
                    throw new Exception();
                }
            } catch (Exception ex) {
                /************************************************
                 * Added to Monitor user's error action
                 * *********************************************/
        //        userService.SendErrorMessageToAdmin(user, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] DaycareUserViewModel model) {
            if (model == null) { return new StatusCodeResult(500); }
            ApplicationUser user = new ApplicationUser();
            user.Id = model.DaycareUserId;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.CreatedDate = DateTime.Today;
            user.LastModifiedDate = DateTime.Today;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Prefix = model.Prefix;
            user.Shimei = model.Shimei;
            user.Myoji = model.Myoji;
            //user.Yubin_Bango = model.Yubin_Bango;
            //user.To_Do_Fu_Ken = model.To_Do_Fu_Ken;
            //user.Shi_Gun_Ku = model.Shi_Gun_Ku;
            //user.Cho_Son = model.Cho_Son;
            //user.Apartment_Etc = model.Apartment_Etc;
            //user.Country = model.Country;
            user.Street = model.Street;
            user.Street2 = model.Street2;
            user.City = model.City;
            user.State = model.State;
            user.Zip = model.Zip;
            user.TelNo = model.TelNo;
            //user.TimezoneName = model.TimezoneName;
            //user.UTCdiff = model.UTCdiff;
            user.Token = model.Token;
            user.OrganizationName = model.OrganizationName;
            user.OrganizationId = model.OrganizationId;
            user.UserType = model.UserType;
            try {
                userService.UpdateUser(user);
                return Ok();
            } catch (Exception ex) {
                /************************************************
              * Added to Monitor user's error action
              * *********************************************/
                userService.SendErrorMessageToAdmin(user, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("createStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] DaycareUserViewModel model) {
            try {
                // 1) Check Organization by Id
                var org = registrationService.getOrganizationById(model.OrganizationId??0);
                if (org == null) { return new StatusCodeResult(500); }
            
            // 2)Add Staff
            if (model == null) { return new StatusCodeResult(500); }
            ApplicationUser user = new ApplicationUser();
            //user.Id = model.Id;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.CreatedDate = DateTime.Today;
            user.LastModifiedDate = DateTime.Today;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Prefix = model.Prefix;
            user.Shimei = model.Shimei;
            user.Myoji = model.Myoji;
            //user.Yubin_Bango = model.Yubin_Bango;
            //user.To_Do_Fu_Ken = model.To_Do_Fu_Ken;
            //user.Shi_Gun_Ku = model.Shi_Gun_Ku;
            //user.Cho_Son = model.Cho_Son;
            //user.Apartment_Etc = model.Apartment_Etc;
            //user.Country = model.Country;
            user.Street = model.Street;
            user.Street2 = model.Street2;
            user.City = model.City;
            user.State = model.State;
            user.Zip = model.Zip;
            user.TelNo = model.TelNo;
            //user.TimezoneName = model.TimezoneName;
            //user.UTCdiff = model.UTCdiff;
            user.Token = model.Token;
            user.OrganizationName = org.OrganizationName;
            user.OrganizationId = model.OrganizationId;
            user.UserType = model.UserType;
            user.RegisteredDate = DateTime.Today;
            user.LastAccessedDate = DateTime.Today;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    var newUser = userService.GetUsersBySearchKey(user).FirstOrDefault();
                    //return Ok(newUser);

                    return Json(newUser, new JsonSerializerOptions {
                        WriteIndented = true,
                    });


                } else {
                    //return BadRequest(result.Errors);
                    throw new Exception();
                }
                return Ok();
            } catch (Exception ex) {
                /************************************************
              * Added to Monitor user's error action
              * *********************************************/
                //userService.SendErrorMessageToAdmin(user, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("createParent")]
        public async Task<IActionResult> CreateParent([FromBody] DaycareUserViewModel model) {
            try {
                // 1) Check Organization by Id
                var org = registrationService.getOrganizationById(model.OrganizationId ?? 0);
                if (org == null) { return new StatusCodeResult(500); }

                // 2)Add Staff
                if (model == null) { return new StatusCodeResult(500); }
                ApplicationUser user = new ApplicationUser();
                //user.Id = model.Id;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.CreatedDate = DateTime.Today;
                user.LastModifiedDate = DateTime.Today;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Prefix = model.Prefix;
                user.Shimei = model.Shimei;
                user.Myoji = model.Myoji;
                //user.Yubin_Bango = model.Yubin_Bango;
                //user.To_Do_Fu_Ken = model.To_Do_Fu_Ken;
                //user.Shi_Gun_Ku = model.Shi_Gun_Ku;
                //user.Cho_Son = model.Cho_Son;
                //user.Apartment_Etc = model.Apartment_Etc;
                //user.Country = model.Country;
                user.Street = model.Street;
                user.Street2 = model.Street2;
                user.City = model.City;
                user.State = model.State;
                user.Zip = model.Zip;
                user.TelNo = model.TelNo;
                //user.TimezoneName = model.TimezoneName;
                //user.UTCdiff = model.UTCdiff;
                user.Token = model.Token;
                user.OrganizationName = org.OrganizationName;
                user.OrganizationId = model.OrganizationId;
                user.UserType = model.UserType;
                user.RegisteredDate = DateTime.Today;
                user.LastAccessedDate = DateTime.Today;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    var newUser = userService.GetUsersBySearchKey(user).FirstOrDefault();
                    //return Ok(newUser);

                    return Json(newUser, new JsonSerializerOptions {
                        WriteIndented = true,
                    });

                } else {
                    //return BadRequest(result.Errors);
                    throw new Exception();
                }
                return Ok();
            } catch (Exception ex) {
                /************************************************
              * Added to Monitor user's error action
              * *********************************************/
                //userService.SendErrorMessageToAdmin(user, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("passwordRecoveryTokenRequest/{email}")]
        public async Task<IActionResult> PasswordRecoveryTokenRequest(string email) {
            try {
                var user = await _userManager.FindByNameAsync(email);
                if (user == null && email.Contains("@")) {
                    user = await _userManager.FindByEmailAsync(email);
                }
                if (user != null) {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    userService.PasswordRecoveryTokenRequest(email, token);
                }
                return Ok(user);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("passwordRecovery")]
        public async Task<IActionResult> PasswordRecovery([FromBody] UserRegistrationViewModel model) {
            if (model == null) { return new StatusCodeResult(500); }
            try {
                var user = await _userManager.FindByNameAsync(model.email);
                var newPassword = model.password;
                var token = model.token;
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (result.Succeeded) {
                    return Ok();
                } else {
                    return BadRequest(result.Errors);
                }
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("updateLastAccessedDate")]
        public async Task<IActionResult> UpdateLastAccessedDate([FromBody] UserRegistrationViewModel model) {
            try {
                ApplicationUser user = new ApplicationUser();
                user.Id = model.id;
                userService.updateLastAccessedDate(user);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("deleteUserById/{Id}")]
        public async Task<IActionResult> DeleteUserById(string Id) {
            if (Id == null) { return new StatusCodeResult(500); }
            ApplicationUser user = new ApplicationUser();
            try {
                userService.DeleteUserById(Id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getUsersBySearchKey")]
        public async Task<IActionResult> GetUsersBySearchKey([FromBody] ApplicationUser model) {
            try {
                if (model == null) { return new StatusCodeResult(500); }
                var user = userService.GetUsersBySearchKey(model);
                return Json(user, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "GetUsersBySearchKey failed");
            }
        }
    }
}