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
                child.ActiveStatus = model.ActiveStatus;


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
                child.ActiveStatus = model.ActiveStatus;

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

        [HttpPost("getTheirChildrenAttendanceRecordByOrganization")]
        public IActionResult getTheirChildrensAttendanceRecordByOrganization([FromBody] Organization model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = childService.getTheirChildrenAttendanceRecordByOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrensAttendanceRecordByOrganization failed");
            }
        }

        [HttpPost("getTheirChildrenAttendanceRecordByChildOrganization")]
        public IActionResult getTheirChildrenAttendanceRecordByChildOrganization([FromBody]Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = childService.getTheirChildrenAttendanceRecordByChildOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrensAttendanceRecordByChildOrganization failed");
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


        [HttpPost("getAttendanceOfTargetChild")]
        public IActionResult getAttendanceOfTargetChild([FromBody]AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.ChildId = model.childId;
                obj.OrganizationId = model.organizationId;
                obj.TargetDate = model.targetDate;
                var response = childService.getAttendanceOfTargetChild(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getAttendanceOfTargetChild failed");
            }
        }

        //[HttpPost("updateAttendanceRecord")]
        //public async Task<IActionResult> updateAttendanceRecord([FromBody]AttendanceRecordViewModel model) {   
        //    try {
        //        if(model == null) { return new StatusCodeResult(500); }
        //        AttendanceRecord obj = new AttendanceRecord();
        //        obj.AttendanceRecordId = model.attendanceRecordId??0;
        //        obj.ChildId = model.childId;
        //        obj.Parent1Id = model.parent1Id;
        //        obj.Parent2Id = model.parent2Id;
        //        obj.OrganizationId = model.organizationId;
        //        obj.ChildFirstName = model.childFirstName;
        //        obj.ChildLastName = model.childLastName;
        //        obj.TargetDate = model.targetDate;
        //        obj.RecordedDate = model.recordedDate;
        //        if(model.inTime==null) {
        //            obj.InTime = null;
        //        } else {
        //            obj.InTime = DateTime.Parse(model.inTime);
        //        }
        //        if(model.outTime == null) {
        //            obj.OutTime = null;
        //        } else {
        //            obj.OutTime = DateTime.Parse(model.outTime);
        //        }
        //        obj.Tardy = model.tardy;
        //        obj.Absent = model.absent;
        //        obj.LeaveEarly = model.leaveEarly;
        //        obj.Reason = model.reason;
        //        obj.Memo = model.memo;
        //        var response = childService.updateAttendanceRecord(obj);
        //        return Json(response,new JsonSerializerOptions {
        //            WriteIndented = true,
        //        });
        //    } catch(Exception ex) {
        //        return BadRequest(ex.Message + "updateAttendanceRecord failed");
        //    }
        //}


        [HttpPost("updateAttendanceRecordIn")]
        public async Task<IActionResult> updateAttendanceRecordIn([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                if(model.inTime == null) {
                    obj.InTime = null;
                } else {
                    obj.InTime = DateTime.Parse(model.inTime);
                }
                if(model.inTime_StampTime == null) {
                    obj.InTime_StampTime = null;
                } else {
                    obj.InTime_StampTime = DateTime.Parse(model.inTime_StampTime);
                }
                obj.InTime_EnteredBy = model.inTime_EnteredBy;
                var response = childService.updateAttendanceRecordIn(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordIn failed");
            }
        }

        [HttpPost("updateAttendanceRecordOut")]
        public async Task<IActionResult> updateAttendanceRecordOut([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                if(model.outTime == null) {
                    obj.OutTime = null;
                } else {
                    obj.OutTime = DateTime.Parse(model.outTime);
                }
                if(model.outTime_StampTime == null) {
                    obj.OutTime_StampTime = null;
                } else {
                    obj.OutTime_StampTime = DateTime.Parse(model.outTime_StampTime);
                }
                obj.OutTime_EnteredBy = model.outTime_EnteredBy;
                var response = childService.updateAttendanceRecordOut(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordOut failed");
            }
        }

        [HttpPost("updateAttendanceRecordTardy")]
        public async Task<IActionResult> updateAttendanceRecordTardy([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.Tardy = model.tardy;
                if(model.tardy_StampTime == null) {
                    obj.Tardy_StampTime = null;
                } else {
                    obj.Tardy_StampTime = DateTime.Parse(model.tardy_StampTime);
                }
                obj.Tardy_EnteredBy = model.tardy_EnteredBy;
                var response = childService.updateAttendanceRecordTardy(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordTardy failed");
            }
        }

        [HttpPost("updateAttendanceRecordAbsent")]
        public async Task<IActionResult> updateAttendanceRecordAbsent([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.Absent = model.absent;
                if(model.absent_StampTime == null) {
                    obj.Absent_StampTime = null;
                } else {
                    obj.Absent_StampTime = DateTime.Parse(model.absent_StampTime);
                }
                obj.Absent_EnteredBy = model.absent_EnteredBy;
                var response = childService.updateAttendanceRecordAbsent(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordAbsent failed");         
            }
        }

        [HttpPost("updateAttendanceRecordLeaveEarly")]
        public async Task<IActionResult> updateAttendanceRecordLeaveEarly([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.LeaveEarly = model.leaveEarly;
                if(model.leaveEarly_StampTime == null) {
                    obj.LeaveEarly_StampTime = null;
                } else {
                    obj.LeaveEarly_StampTime = DateTime.Parse(model.leaveEarly_StampTime);
                }
                obj.LeaveEarly_EnteredBy = model.leaveEarly_EnteredBy;
                var response = childService.updateAttendanceRecordLeaveEarly(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordOut failed");
            }
        }

        [HttpPost("saveProfileFilePath")]
        public async Task<IActionResult> saveProfileFilePath([FromBody]Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                childService.saveProfileFilePath(model);
                return Ok();
            } catch(Exception ex) {
                return BadRequest(ex.Message + "saveProfileFilePath failed");
            }
        }

        [HttpPost("saveTardyComment")]
        public async Task<IActionResult> saveTardyComment([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.TardyComment = model.tardyComment;
                if(model.tardy_StampTime == null) {
                    obj.Tardy_StampTime = null;
                } else {
                    obj.Tardy_StampTime = DateTime.Parse(model.tardy_StampTime);
                }
                obj.Tardy_EnteredBy = model.tardy_EnteredBy;
                var response = childService.saveTardyComment(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "saveTardyComment failed");
            }
        }

        [HttpPost("saveAbsentComment")]
        public async Task<IActionResult> saveAbsentComment([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.AbsentComment = model.absentComment;
                if(model.absent_StampTime == null) {
                    obj.Absent_StampTime = null;
                } else {
                    obj.Absent_StampTime = DateTime.Parse(model.absent_StampTime);
                }
                obj.Absent_EnteredBy = model.absent_EnteredBy;
                var response = childService.saveAbsentComment(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "saveAbsentComment failed");
            }
        }

        [HttpPost("saveLeaveEarlyComment")]
        public async Task<IActionResult> saveLeaveEarlyComment([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                obj.LeaveEarlyComment = model.leaveEarlyComment;
                if(model.leaveEarly_StampTime == null) {
                    obj.LeaveEarly_StampTime = null;
                } else {
                    obj.LeaveEarly_StampTime = DateTime.Parse(model.leaveEarly_StampTime);
                }
                obj.LeaveEarly_EnteredBy = model.leaveEarly_EnteredBy;        
                var response = childService.saveLeaveEarlyComment(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "saveLeaveEarlyComment failed");
            }
        }

        [HttpPost("cancelSignIn")]
        public async Task<IActionResult> cancelSignIn([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                if(model.cancelInTime_StampTime == null) {
                    obj.CancelInTime_StampTime = null;
                } else {
                    obj.CancelInTime_StampTime = DateTime.Parse(model.cancelInTime_StampTime);
                }
                obj.CancelInTime_EnteredBy = model.cancelInTime_EnteredBy;
                var response = childService.cancelSignIn(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "cancelSignIn failed");
            }
        }

        [HttpPost("cancelSignOut")]
        public async Task<IActionResult> cancelSignOut([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.AttendanceRecordId = model.attendanceRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;
                if(model.cancelOutTime_StampTime == null) {
                    obj.CancelOutTime_StampTime = null;
                } else {
                    obj.CancelOutTime_StampTime = DateTime.Parse(model.cancelOutTime_StampTime);
                }
                obj.CancelOutTime_EnteredBy = model.cancelOutTime_EnteredBy;
                var response = childService.cancelSignOut(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "cancelSignOut failed");
            }
        }


    }
}
