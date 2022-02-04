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
    public class AttendanceController:Controller {

        IAttendanceService attendanceService;
        public AttendanceController(IAttendanceService attendanceService) {
            this.attendanceService = attendanceService;
        }

        [HttpPost("getTheirChildrenAttendanceRecordByOrganization")]
        public IActionResult getTheirChildrensAttendanceRecordByOrganization([FromBody] Organization model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = attendanceService.getTheirChildrenAttendanceRecordByOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrensAttendanceRecordByOrganization failed");
            }
        }

        [HttpPost("getTheirChildrenAttendanceRecordByChildOrganization")]
        public IActionResult getTheirChildrenAttendanceRecordByChildOrganization([FromBody] Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = attendanceService.getTheirChildrenAttendanceRecordByChildOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrensAttendanceRecordByChildOrganization failed");
            }
        }

        [HttpPost("getAttendanceOfTargetChild")]
        public IActionResult getAttendanceOfTargetChild([FromBody] AttendanceRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                AttendanceRecord obj = new AttendanceRecord();
                obj.ChildId = model.childId;
                obj.OrganizationId = model.organizationId;
                obj.TargetDate = model.targetDate;
                var response = attendanceService.getAttendanceOfTargetChild(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getAttendanceOfTargetChild failed");
            }
        }

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
                var response = attendanceService.updateAttendanceRecordIn(obj);
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
                var response = attendanceService.updateAttendanceRecordOut(obj);
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
                var response = attendanceService.updateAttendanceRecordTardy(obj);
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
                var response = attendanceService.updateAttendanceRecordAbsent(obj);
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
                var response = attendanceService.updateAttendanceRecordLeaveEarly(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecordOut failed");
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
                var response = attendanceService.saveTardyComment(obj);
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
                var response = attendanceService.saveAbsentComment(obj);
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
                var response = attendanceService.saveLeaveEarlyComment(obj);
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
                var response = attendanceService.cancelSignIn(obj);
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
                var response = attendanceService.cancelSignOut(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "cancelSignOut failed");
            }
        }


    }
}
