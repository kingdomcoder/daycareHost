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
using System.Linq;

namespace Daycare.WebAPIHost.Controllers {

    //  [AllowAnonymous]
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class NapController:Controller {

        INapService napService;
        public NapController(INapService napService) {
            this.napService = napService;
        }

        [HttpPost("getTheirChildrenNapRecordByOrganization")]
        public IActionResult getTheirChildrenNapRecordByOrganization([FromBody] Organization model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = napService.getTheirChildrenNapRecordByOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenNapRecordByOrganization failed");
            }
        }

        [HttpPost("getTheirChildrenNapRecordByChildOrganization")]
        public IActionResult getTheirChildrenNapRecordByChildOrganization([FromBody] Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = napService.getTheirChildrenNapRecordByChildOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenNapRecordByChildOrganization failed");
            }
        }

        [HttpPost("getNapOfTargetChild")]
        public IActionResult getNapOfTargetChild([FromBody] NapRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                NapRecord obj = new NapRecord();
                obj.ChildId = model.childId;
                obj.OrganizationId = model.organizationId;
                obj.TargetDate = model.targetDate;
                obj.NapType = model.napType; //Need this for edit
                var response = napService.getNapOfTargetChild(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getNapOfTargetChild failed");
            }
        }

        [HttpPost("saveNapRecord")]
        public async Task<IActionResult> saveNapRecord([FromBody] NapRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                NapRecord obj = new NapRecord();
                //  obj.NapRecordId = model.napRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;

                obj.NapType = model.napType;
                if(model.startStampTime != null) {
                    obj.StartStampTime = DateTime.Parse(model.startStampTime);
                }
                if(model.endStampTime != null) {
                    obj.EndStampTime = DateTime.Parse(model.endStampTime);
                }
                obj.Description = model.description;
                obj.VisibleToStaffOnly = model.visibleToStaffOnly;
                if(model.createdDate != null) {
                    obj.CreatedDate = DateTime.Parse(model.createdDate);
                }
                obj.CreatedBy = model.createdBy;

                if(model.firstStartStampTime != null) {
                    obj.FirstStartStampTime = DateTime.Parse(model.firstStartStampTime);
                }
                if(model.firstEndStampTime != null) {
                    obj.FirstEndStampTime = DateTime.Parse(model.firstEndStampTime);
                }
                obj.FirstDescription = model.firstDescription;
                obj.FirstVisibleToStaffOnly = model.firstVisibleToStaffOnly;
                if(model.firstCreatedDate != null) {
                    obj.FirstCreatedDate = DateTime.Parse(model.firstCreatedDate);
                }
                obj.FirstCreatedBy = model.firstCreatedBy;

                if(model.secondStartStampTime != null) {
                    obj.SecondStartStampTime = DateTime.Parse(model.secondStartStampTime);
                }
                if(model.secondEndStampTime != null) {
                    obj.SecondEndStampTime = DateTime.Parse(model.secondEndStampTime);
                }
                obj.SecondDescription = model.secondDescription;
                obj.SecondVisibleToStaffOnly = model.secondVisibleToStaffOnly;
                if(model.secondCreatedDate != null) {
                    obj.SecondCreatedDate = DateTime.Parse(model.secondCreatedDate);
                }
                obj.SecondCreatedBy = model.secondCreatedBy;

                if(model.thirdStartStampTime != null) {
                    obj.ThirdStartStampTime = DateTime.Parse(model.thirdStartStampTime);
                }
                if(model.thirdEndStampTime != null) {
                    obj.ThirdEndStampTime = DateTime.Parse(model.thirdEndStampTime);
                }
                obj.ThirdDescription = model.thirdDescription;
                obj.ThirdVisibleToStaffOnly = model.thirdVisibleToStaffOnly;
                if(model.thirdCreatedDate != null) {
                    obj.ThirdCreatedDate = DateTime.Parse(model.thirdCreatedDate);
                }
                obj.ThirdCreatedBy = model.thirdCreatedBy;

                var response = napService.saveNapRecord(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecord failed");
            }
        }


    }
}
