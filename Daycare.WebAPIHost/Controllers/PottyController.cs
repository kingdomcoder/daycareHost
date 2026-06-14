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
    [Authorize]
    [EnableCors("AllowAllOrigins")] // Defined in startup.cs
    [Route("api/[controller]")]
    public class PottyController:Controller {

        IPottyService pottyService;
        public PottyController(IPottyService pottyService) {
            this.pottyService = pottyService;
        }

        [HttpPost("getTheirChildrenPottyRecordByOrganization")]
        public IActionResult getTheirChildrenPottyRecordByOrganization([FromBody] Organization model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = pottyService.getTheirChildrenPottyRecordByOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenPottyRecordByOrganization failed");
            }
        }

        [HttpPost("getTheirChildrenPottyRecordByChildOrganization")]
        public IActionResult getTheirChildrenPottyRecordByChildOrganization([FromBody] Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = pottyService.getTheirChildrenPottyRecordByChildOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenPottyRecordByChildOrganization failed");
            }
        }

        [HttpPost("getPottyOfTargetChild")]
        public IActionResult getPottyOfTargetChild([FromBody] PottyRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                PottyRecord obj = new PottyRecord();
                obj.ChildId = model.childId;
                obj.OrganizationId = model.organizationId;
                obj.TargetDate = model.targetDate;
                obj.PottyType = model.pottyType; //Need this for edit
                var response = pottyService.getPottyOfTargetChild(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getPottyOfTargetChild failed");
            }
        }

        [HttpPost("savePottyRecord")]
        public async Task<IActionResult> savePottyRecord([FromBody] PottyRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                PottyRecord obj = new PottyRecord();
                //  obj.PottyRecordId = model.pottyRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;

                obj.PottyType = model.pottyType;
                if(model.stampTime != null) {
                    obj.StampTime = DateTime.Parse(model.stampTime);
                }
                obj.Description = model.description;
                obj.VisibleToStaffOnly = model.visibleToStaffOnly;
                if(model.createdDate != null) {
                    obj.CreatedDate = DateTime.Parse(model.createdDate);
                }
                obj.CreatedBy = model.createdBy;


                if(model.firstStampTime != null) {
                    obj.FirstStampTime = DateTime.Parse(model.firstStampTime);
                }
                obj.FirstDescription = model.firstDescription;
                obj.FirstVisibleToStaffOnly = model.firstVisibleToStaffOnly;
                if(model.firstCreatedDate != null) {
                    obj.FirstCreatedDate = DateTime.Parse(model.firstCreatedDate);
                }
                obj.FirstCreatedBy = model.firstCreatedBy;


                if(model.secondStampTime != null) {
                    obj.SecondStampTime = DateTime.Parse(model.secondStampTime);
                }
                obj.SecondDescription = model.secondDescription;
                obj.SecondVisibleToStaffOnly = model.secondVisibleToStaffOnly;
                if(model.secondCreatedDate != null) {
                    obj.SecondCreatedDate = DateTime.Parse(model.secondCreatedDate);
                }
                obj.SecondCreatedBy = model.secondCreatedBy;

                if(model.thirdStampTime != null) {
                    obj.ThirdStampTime = DateTime.Parse(model.thirdStampTime);
                }
                obj.ThirdDescription = model.thirdDescription;
                obj.ThirdVisibleToStaffOnly = model.thirdVisibleToStaffOnly;
                if(model.thirdCreatedDate != null) {
                    obj.ThirdCreatedDate = DateTime.Parse(model.thirdCreatedDate);
                }
                obj.ThirdCreatedBy = model.thirdCreatedBy;

                if(model.forthStampTime != null) {
                    obj.ForthStampTime = DateTime.Parse(model.forthStampTime);
                }
                obj.ForthDescription = model.forthDescription;
                obj.ForthVisibleToStaffOnly = model.forthVisibleToStaffOnly;
                if(model.forthCreatedDate != null) {
                    obj.ForthCreatedDate = DateTime.Parse(model.forthCreatedDate);
                }
                obj.ForthCreatedBy = model.forthCreatedBy;

                if(model.fifthStampTime != null) {
                    obj.FifthStampTime = DateTime.Parse(model.fifthStampTime);
                }
                obj.FifthDescription = model.fifthDescription;
                obj.FifthVisibleToStaffOnly = model.fifthVisibleToStaffOnly;
                if(model.fifthCreatedDate != null) {
                    obj.FifthCreatedDate = DateTime.Parse(model.fifthCreatedDate);
                }
                obj.FifthCreatedBy = model.fifthCreatedBy;


                var response = pottyService.savePottyRecord(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecord failed");
            }
        }


    }
}
  