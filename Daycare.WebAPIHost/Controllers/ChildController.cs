using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Services.Abstract;
using Daycare.WebAPIHost.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daycare.WebAPIHost.Controllers {
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class ChildController : Controller {

        IChildService childService;
        public ChildController(IChildService childService) {
            this.childService = childService;
        }

        [AllowAnonymous]
        [HttpPost("createChild")]
        public async Task<IActionResult> CreateChild([FromBody] ChildRegistationViewModel model) {
            Child child = new Child();
            try {
                if (model == null) { return new StatusCodeResult(500); }
                child.Parent1Id = model.Parent1Id;
                child.Parent2Id = model.Parent2Id;
                child.ChildFirstName = model.ChildFirstName;
                child.ChildLastName = model.ChildLastName;
                child.ChildMiddleName = model.ChildMiddleName;
                child.ChildShimei = model.ChildShimei;
                child.ChildMyoji = model.ChildMyoji;
                child.Gender = model.Gender;
                if (model.DOB != null) {
                    /********************************************************************************
                     * This adjustment is because wherever user is, default time for insert is 8:00am.
                     * Need to add 4hours so that UTC becomes 12:00pm. 12:00pm is best because wherever
                     * user is, it returns same date.
                     * *******************************************************************************/
                  //  model.DOB = model.DOB.Value.AddHours(4);
                    model.DOB = model.DOB.Value.ToUniversalTime();
                }
                child.DOB = model.DOB;
                child.Grade = model.Grade;
                child.ClassName = model.ClassName;
                child.AttendMon = model.AttendMon;
                child.AttendTue = model.AttendTue;
                child.AttendWed = model.AttendWed;
                child.AttendThu = model.AttendThu;
                child.AttendFri = model.AttendFri;
                child.AttendSat = model.AttendSat;
                child.AttendSun = model.AttendSun;
                child.OrganizationId = model.OrganizationId;
                child.RegisteredDate = DateTime.Now.ToUniversalTime();


                childService.CreateChild(child);
                return Ok();
            } catch (Exception ex) {
                /************************************************
                 * Added to Monitor user's error action
                 * *********************************************/
                childService.SendErrorMessageToAdmin(child, ex.Message);
                return BadRequest(ex.Message + "CreateChild failed.");
            }
        }

        [AllowAnonymous]
        [HttpPost("updateChild")]
        public async Task<IActionResult> UpdateChild([FromBody] ChildRegistationViewModel model) {
            if (model == null) { return new StatusCodeResult(500); }
            Child child = new Child();
            try {
                if (model == null) { return new StatusCodeResult(500); }
                child.Parent1Id = model.Parent1Id;
                child.Parent2Id = model.Parent2Id;
                child.ChildFirstName = model.ChildFirstName;
                child.ChildLastName = model.ChildLastName;
                child.ChildMiddleName = model.ChildMiddleName;
                child.ChildShimei = model.ChildShimei;
                child.ChildMyoji = model.ChildMyoji;
                child.Gender = model.Gender;
                if (model.DOB != null) {
                    /********************************************************************************
                     * This adjustment is because wherever user is, default time for insert is 8:00am.
                     * Need to add 4hours so that UTC becomes 12:00pm. 12:00pm is best because wherever
                     * user is, it returns same date.
                     * *******************************************************************************/
                    model.DOB = model.DOB.Value.AddHours(4);
                }
                child.DOB = model.DOB;
                child.Grade = model.Grade;
                child.ClassName = model.ClassName;
                child.AttendMon = model.AttendMon;
                child.AttendTue = model.AttendTue;
                child.AttendWed = model.AttendWed;
                child.AttendThu = model.AttendThu;
                child.AttendFri = model.AttendFri;
                child.AttendSat = model.AttendSat;
                child.AttendSun = model.AttendSun;
                child.RegisteredDate = DateTime.Now;


                childService.UpdateChild(child);
                return Ok();
            } catch (Exception ex) {
                /************************************************
                 * Added to Monitor user's error action
                 * *********************************************/
                childService.SendErrorMessageToAdmin(child, ex.Message);
                return BadRequest(ex.Message + "UpdateChild failed.");
            }
        }

        [HttpGet("getMyChildrenByParentId/{Id}")]
        public IActionResult GetMyChildrenByParentId(string Id) {
            try {
                if (Id == null) { return new StatusCodeResult(500); }
                var children = childService.GetMyChildrenByParentId(Id);
                return Json(children, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "GetMyChildrenByParentId failed");
            }
        }

        [HttpPost("getMyChildrenByParentUser")]
        public IActionResult getMyChildrenByParentUser([FromBody] DaycareUserViewModel model) {
            try {
                if (model == null) { return new StatusCodeResult(500); }
                ApplicationUser user = new ApplicationUser();
                user.Id = model.DaycareUserId;
                user.OrganizationId = model.OrganizationId;
                var children = childService.GetMyChildrenByParentUser(user);
                return Json(children, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "GetMyChildrenByParentId failed");
            }
        }


        [HttpPost("getTheirChildrenByOrganization")]
        public IActionResult getTheirChildrenByOrganization([FromBody]Organization model) {
            try {
                if (model == null) { return new StatusCodeResult(500); }
                var children = childService.getTheirChildrenByOrganization(model);
                return Json(children, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenByOrganization failed");
            }
        }

        //[HttpGet("getChildActivityLogByChild")]
        //public IActionResult GetChildActivityLogByChild([FromBody]ChildRegistationViewModel model) {
        //    try {
        //        if (model == null) { return new StatusCodeResult(500); }
        //        Child child = new Child();
        //        child.ChildId = model.ChildId??0;
        //        var children = childService.GetChildActivityLogByChild(child, model.TargetDate);
        //        return Json(children, new JsonSerializerOptions {
        //            WriteIndented = true,
        //        });
        //    } catch (Exception ex) {
        //        return BadRequest(ex.Message + "getChildActivityLogByChildId failed");
        //    }
        //}

        [HttpPost("postMessage")]
        public async Task<IActionResult> PostMessage([FromBody] CommentRecord model) {
            try {
                if (model == null) { return new StatusCodeResult(500); }
                childService.PostMessage(model);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message + "postMessage failed");
            }
        }


        [HttpGet("getChildByChildId/{id}")]
        public IActionResult GetChildByChildId(int id) {
            try {
                if (id == null) { return new StatusCodeResult(500); }
                var children = childService.GetChildByChildId(id);
                return Json(children, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "GetChildByChildId failed");
            }
        }

        [HttpPost("getActivityByChild")]
        public IActionResult getActivityByChild([FromBody]Child model) {
            try {
                if (model == null) { return new StatusCodeResult(500); }
                var children = childService.getActivityByChild(model);
                return Json(children, new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch (Exception ex) {
                return BadRequest(ex.Message + "getActivityByChild failed");
            }
        }
    }
}
