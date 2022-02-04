using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare.Chat;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFAttendanceRepository:IAttendanceRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFAttendanceRepository(MyDbContext context,MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByOrganization(Organization model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join attendanceRecord in context.AttendanceRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = attendanceRecord.OrganizationId,
                                  ChildId = attendanceRecord.ChildId ?? 0
                              } into AttendanceRecord_join
                              from attendanceRecord_join in AttendanceRecord_join.DefaultIfEmpty()
                              where child.OrganizationId == model.OrganizationId &&
                              child.ActiveStatus == true &&
                                    (strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                    (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                    (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                    (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                    (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                    (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null)
                              select new AttendanceRecord() {
                                  AttendanceRecordId = attendanceRecord_join.AttendanceRecordId,
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = attendanceRecord_join.ImagePath,
                                  ImageFileName = attendanceRecord_join.ImageFileName,
                                  TargetDate = child.TargetDate,
                                  InTime = attendanceRecord_join.InTime,
                                  InTime_StampTime = attendanceRecord_join.InTime_StampTime,
                                  InTime_EnteredBy = attendanceRecord_join.InTime_EnteredBy,
                                  OutTime = attendanceRecord_join.OutTime,
                                  OutTime_StampTime = attendanceRecord_join.OutTime_StampTime,
                                  OutTime_EnteredBy = attendanceRecord_join.OutTime_EnteredBy,
                                  Tardy = attendanceRecord_join.Tardy,
                                  TardyComment = attendanceRecord_join.TardyComment,
                                  Tardy_StampTime = attendanceRecord_join.Tardy_StampTime,
                                  Tardy_EnteredBy = attendanceRecord_join.Tardy_EnteredBy,
                                  Absent = attendanceRecord_join.Absent,
                                  AbsentComment = attendanceRecord_join.AbsentComment,
                                  Absent_StampTime = attendanceRecord_join.Absent_StampTime,
                                  Absent_EnteredBy = attendanceRecord_join.Absent_EnteredBy,
                                  LeaveEarly = attendanceRecord_join.LeaveEarly,
                                  LeaveEarlyComment = attendanceRecord_join.LeaveEarlyComment,
                                  LeaveEarly_StampTime = attendanceRecord_join.LeaveEarly_StampTime,
                                  LeaveEarly_EnteredBy = attendanceRecord_join.LeaveEarly_EnteredBy,
                                  CancelInTime_StampTime = attendanceRecord_join.CancelInTime_StampTime,
                                  CancelInTime_EnteredBy = attendanceRecord_join.CancelInTime_EnteredBy,
                                  CancelOutTime_StampTime = attendanceRecord_join.CancelOutTime_StampTime,
                                  CancelOutTime_EnteredBy = attendanceRecord_join.CancelOutTime_EnteredBy,
                              }).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByChildOrganization(Child model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join attendanceRecord in context.AttendanceRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = attendanceRecord.OrganizationId,
                                  ChildId = attendanceRecord.ChildId ?? 0
                              } into AttendanceRecord_join
                              from attendanceRecord_join in AttendanceRecord_join.DefaultIfEmpty()
                              where
                              (child.OrganizationId == model.OrganizationId) &&
                              ((model.ChildId == null || model.ChildId == 0) ? null == null : child.ChildId == model.ChildId) &&
                              child.ActiveStatus == true
                                &&
                                ((strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null))

                              select new AttendanceRecord() {
                                  //   AttendanceRecordId = attendanceRecord_join.AttendanceRecordId, //Need to delete becuae it should not be null
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = child.ImagePath,
                                  ImageFileName = child.ImageFileName,
                              }).ToList();

                var query = (from g in result
                             group g by new {
                                 g.ChildId,
                                 g.OrganizationId,
                                 g.Parent1Id,
                                 g.Parent2Id,
                                 g.ChildFirstName,
                                 g.ChildLastName,
                                 g.ImagePath,
                                 g.ImageFileName,
                             }
                            into grp
                             select new AttendanceRecord() {
                                 ChildId = grp.Key.ChildId,
                                 OrganizationId = grp.Key.OrganizationId,
                                 Parent1Id = grp.Key.Parent1Id,
                                 Parent2Id = grp.Key.Parent2Id,
                                 ChildFirstName = grp.Key.ChildFirstName,
                                 ChildLastName = grp.Key.ChildLastName,
                                 ImagePath = grp.Key.ImagePath,
                                 ImageFileName = grp.Key.ImageFileName,
                             }).ToList();
                foreach(var obj in query) {
                    var result2 = (from attendanceRecord in context.AttendanceRecord
                                   where
                                   attendanceRecord.OrganizationId == obj.OrganizationId &&
                                   attendanceRecord.ChildId == obj.ChildId &&
                                   attendanceRecord.TargetDate == model.TargetDate
                                   select attendanceRecord).FirstOrDefault();
                    if(result2 != null) {
                        obj.InTime = result2.InTime;
                        obj.InTime_StampTime = result2.InTime_StampTime;
                        obj.InTime_EnteredBy = result2.InTime_EnteredBy;
                        obj.OutTime = result2.OutTime;
                        obj.OutTime_StampTime = result2.OutTime_StampTime;
                        obj.OutTime_EnteredBy = result2.OutTime_EnteredBy;
                        obj.Tardy = result2.Tardy;
                        obj.TardyComment = result2.TardyComment;
                        obj.Tardy_StampTime = result2.Tardy_StampTime;
                        obj.Tardy_EnteredBy = result2.Tardy_EnteredBy;
                        obj.Absent = result2.Absent;
                        obj.AbsentComment = result2.AbsentComment;
                        obj.Absent_StampTime = result2.Absent_StampTime;
                        obj.Absent_EnteredBy = result2.Absent_EnteredBy;
                        obj.LeaveEarly = result2.LeaveEarly;
                        obj.LeaveEarlyComment = result2.LeaveEarlyComment;
                        obj.LeaveEarly_StampTime = result2.LeaveEarly_StampTime;
                        obj.LeaveEarly_EnteredBy = result2.LeaveEarly_EnteredBy;
                        obj.CreatedDate = result2.CreatedDate;
                        obj.UpdatedDate = result2.UpdatedDate;
                    }
                }
                return query;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void PostMessage(CommentRecord model) {
            try {
                CommentRecord dbEntry = (from table in context.CommentRecord
                                         where
                                         table.ChildId == model.ChildId
                                         select table
                  ).FirstOrDefault();

                if(dbEntry == null || model.ChildId == null) {
                    context.CommentRecord.Add(model);
                    context.SaveChanges();
                }
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public Child GetChildByChildId(int id) {
            try {
                var child = (from table in context.Child
                             where table.ChildId == id
                             select table).FirstOrDefault();
                return child;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Activity> getActivityByChild(Child model) {
            try {
                List<Activity> logs = new List<Activity>();

                // 1-1) AttendanceRecord In
                var attendancelogIn = (from table in context.AttendanceRecord
                                       where table.ChildId == model.ChildId &&
                                       table.OrganizationId == model.OrganizationId &&
                                       table.TargetDate == model.TargetDate &&
                                       table.InTime != null
                                       select table).ToList();
                if(attendancelogIn.Count > 0) {
                    foreach(var attendancelog in attendancelogIn) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Sign-In";
                        activitylog.CreatedDate = attendancelog.InTime;
                        activitylog.CreatedBy = attendancelog.InTime_EnteredBy;
                        logs.Add(activitylog);
                    }
                }

                // 1-2) AttendanceRecord Out
                var attendancelogOut = (from table in context.AttendanceRecord
                                        where table.ChildId == model.ChildId &&
                                        table.OrganizationId == model.OrganizationId &&
                                        table.TargetDate == model.TargetDate &&
                                        table.OutTime != null
                                        select table).ToList();
                if(attendancelogOut.Count > 0) {
                    foreach(var attendancelog in attendancelogOut) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Sing-Out";
                        activitylog.CreatedDate = attendancelog.OutTime;
                        activitylog.CreatedBy = attendancelog.OutTime_EnteredBy;
                        logs.Add(activitylog);
                    }
                }

                // 1-3) AttendanceRecord Tardy
                var attendancelogTardy = (from table in context.AttendanceRecord
                                          where table.ChildId == model.ChildId &&
                                          table.OrganizationId == model.OrganizationId &&
                                          table.TargetDate == model.TargetDate &&
                                          table.Tardy == true &&
                                          table.Tardy_StampTime != null
                                          select table).ToList();
                if(attendancelogTardy.Count > 0) {
                    foreach(var attendancelog in attendancelogTardy) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Tardy";
                        activitylog.CreatedDate = attendancelog.Tardy_StampTime;
                        activitylog.CreatedBy = attendancelog.Tardy_EnteredBy;
                        activitylog.ActivityDescription = attendancelog.TardyComment;
                        logs.Add(activitylog);
                    }
                }

                // 1-4) AttendanceRecord Absent
                var attendancelogAbsent = (from table in context.AttendanceRecord
                                           where table.ChildId == model.ChildId &&
                                           table.OrganizationId == model.OrganizationId &&
                                           table.TargetDate == model.TargetDate &&
                                           table.Absent == true &&
                                           table.Absent_StampTime != null
                                           select table).ToList();
                if(attendancelogAbsent.Count > 0) {
                    foreach(var attendancelog in attendancelogAbsent) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Absent";
                        activitylog.CreatedDate = attendancelog.Absent_StampTime;
                        activitylog.CreatedBy = attendancelog.Absent_EnteredBy;
                        activitylog.ActivityDescription = attendancelog.AbsentComment;
                        logs.Add(activitylog);
                    }
                }

                // 1-5) AttendanceRecord Leave
                var attendancelogLeave = (from table in context.AttendanceRecord
                                          where table.ChildId == model.ChildId &&
                                          table.OrganizationId == model.OrganizationId &&
                                          table.TargetDate == model.TargetDate &&
                                          table.LeaveEarly == true &&
                                          table.LeaveEarly_StampTime != null
                                          select table).ToList();
                if(attendancelogAbsent.Count > 0) {
                    foreach(var attendancelog in attendancelogAbsent) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Leave Early";
                        activitylog.CreatedDate = attendancelog.LeaveEarly_StampTime;
                        activitylog.CreatedBy = attendancelog.LeaveEarly_EnteredBy;
                        activitylog.ActivityDescription = attendancelog.LeaveEarlyComment;
                        logs.Add(activitylog);
                    }
                }


                // 2-1) BealRecord - Breakfast
                var breakfasts = (from table in context.MealRecord
                                  where table.ChildId == model.ChildId &&
                                  table.OrganizationId == model.OrganizationId &&
                                  table.TargetDate == model.TargetDate &&
                                  table.Breakfast == true
                                  select table).ToList();
                if(breakfasts.Count > 0) {
                    foreach(var breakfast in breakfasts) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Breakfast";
                        activitylog.CreatedDate = breakfast.BreakfastCreatedDate;
                        activitylog.CreatedBy = breakfast.BreakfastCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 2-2) BealRecord - AM Snack
                var amSnacks = (from table in context.MealRecord
                                where table.ChildId == model.ChildId &&
                                table.OrganizationId == model.OrganizationId &&
                                table.TargetDate == model.TargetDate &&
                                table.AMSnack == true
                                select table).ToList();
                if(amSnacks.Count > 0) {
                    foreach(var amSnack in amSnacks) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "AM Snack";
                        activitylog.CreatedDate = amSnack.AMSnackCreatedDate;
                        activitylog.CreatedBy = amSnack.AMSnackCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 2-3) BealRecord - AM Snack
                var lunchs = (from table in context.MealRecord
                              where table.ChildId == model.ChildId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.TargetDate == model.TargetDate &&
                              table.Lunch == true
                              select table).ToList();
                if(lunchs.Count > 0) {
                    foreach(var lunch in lunchs) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Lunch";
                        activitylog.CreatedDate = lunch.LunchCreatedDate;
                        activitylog.CreatedBy = lunch.LunchCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 2-4) BealRecord - PM Snack
                var pmSnacks = (from table in context.MealRecord
                                where table.ChildId == model.ChildId &&
                                table.OrganizationId == model.OrganizationId &&
                                table.TargetDate == model.TargetDate &&
                                table.PMSnack == true
                                select table).ToList();
                if(pmSnacks.Count > 0) {
                    foreach(var pmSnack in pmSnacks) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "PM Snack";
                        activitylog.CreatedDate = pmSnack.PMSnackCreatedDate;
                        activitylog.CreatedBy = pmSnack.PMSnackCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 2-5) BealRecord - Late Snack
                var lateSnacks = (from table in context.MealRecord
                                  where table.ChildId == model.ChildId &&
                                  table.OrganizationId == model.OrganizationId &&
                                  table.TargetDate == model.TargetDate &&
                                  table.LateSnack == true
                                  select table).ToList();
                if(lateSnacks.Count > 0) {
                    foreach(var lateSnack in lateSnacks) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Late Snack";
                        activitylog.CreatedDate = lateSnack.LateSnackCreatedDate;
                        activitylog.CreatedBy = lateSnack.LateSnackCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 2-6) BealRecord - Dinner
                var dinners = (from table in context.MealRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate &&
                               table.Dinner == true
                               select table).ToList();
                if(dinners.Count > 0) {
                    foreach(var dinner in dinners) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Dinner";
                        activitylog.CreatedDate = dinner.DinnerCreatedDate;
                        activitylog.CreatedBy = dinner.DinnerCreatedBy;
                        logs.Add(activitylog);
                    }
                }


                // 3-1) NapRecord - first
                var napRecords1 = (from table in context.NapRecord
                                   where table.ChildId == model.ChildId &&
                                   table.OrganizationId == model.OrganizationId &&
                                   table.TargetDate == model.TargetDate &&
                                   table.FirstStartStampTime != null
                                   select table).ToList();
                if(napRecords1.Count > 0) {
                    foreach(var napRecord in napRecords1) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Nap";
                        activitylog.StartTime = napRecord.FirstStartStampTime;
                        activitylog.EndTime = napRecord.FirstEndStampTime;
                        activitylog.ActivityDescription = napRecord.FirstDescription;
                        activitylog.CreatedDate = napRecord.FirstCreatedDate;
                        activitylog.CreatedBy = napRecord.FirstCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 3-2) NapRecord - second
                var napRecords2 = (from table in context.NapRecord
                                   where table.ChildId == model.ChildId &&
                                   table.OrganizationId == model.OrganizationId &&
                                   table.TargetDate == model.TargetDate &&
                                   table.SecondStartStampTime != null
                                   select table).ToList();
                if(napRecords2.Count > 0) {
                    foreach(var napRecord in napRecords2) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Nap";
                        activitylog.StartTime = napRecord.SecondStartStampTime;
                        activitylog.EndTime = napRecord.SecondEndStampTime;
                        activitylog.ActivityDescription = napRecord.SecondDescription;
                        activitylog.CreatedDate = napRecord.SecondCreatedDate;
                        activitylog.CreatedBy = napRecord.SecondCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 3-3) NapRecord - third
                var napRecords3 = (from table in context.NapRecord
                                   where table.ChildId == model.ChildId &&
                                   table.OrganizationId == model.OrganizationId &&
                                   table.TargetDate == model.TargetDate &&
                                   table.ThirdStartStampTime != null
                                   select table).ToList();
                if(napRecords3.Count > 0) {
                    foreach(var napRecord in napRecords3) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Nap";
                        activitylog.StartTime = napRecord.ThirdStartStampTime;
                        activitylog.EndTime = napRecord.ThirdEndStampTime;
                        activitylog.ActivityDescription = napRecord.ThirdDescription;
                        activitylog.CreatedDate = napRecord.ThirdCreatedDate;
                        activitylog.CreatedBy = napRecord.ThirdCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 4-1) PottyRecord - first
                var pottyRecords1 = (from table in context.PottyRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate &&
                                     table.FirstStampTime != null
                                     select table).ToList();
                if(pottyRecords1.Count > 0) {
                    foreach(var pottyRecord in pottyRecords1) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Potty";
                        activitylog.StartTime = pottyRecord.FirstStampTime;
                        activitylog.ActivityDescription = pottyRecord.FirstDescription;
                        activitylog.CreatedDate = pottyRecord.FirstCreatedDate;
                        activitylog.CreatedBy = pottyRecord.FirstCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 4-2) PottyRecord - second
                var pottyRecords2 = (from table in context.PottyRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate &&
                                     table.SecondStampTime != null
                                     select table).ToList();
                if(pottyRecords2.Count > 0) {
                    foreach(var pottyRecord in pottyRecords2) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Potty";
                        activitylog.StartTime = pottyRecord.SecondStampTime;
                        activitylog.ActivityDescription = pottyRecord.SecondDescription;
                        activitylog.CreatedDate = pottyRecord.SecondCreatedDate;
                        activitylog.CreatedBy = pottyRecord.SecondCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 4-3) PottyRecord - third
                var pottyRecords3 = (from table in context.PottyRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate &&
                                     table.ThirdStampTime != null
                                     select table).ToList();
                if(pottyRecords3.Count > 0) {
                    foreach(var pottyRecord in pottyRecords3) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Potty";
                        activitylog.StartTime = pottyRecord.ThirdStampTime;
                        activitylog.ActivityDescription = pottyRecord.ThirdDescription;
                        activitylog.CreatedDate = pottyRecord.ThirdCreatedDate;
                        activitylog.CreatedBy = pottyRecord.ThirdCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 4-4) PottyRecord - forth
                var pottyRecords4 = (from table in context.PottyRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate &&
                                     table.ForthStampTime != null
                                     select table).ToList();
                if(pottyRecords4.Count > 0) {
                    foreach(var pottyRecord in pottyRecords4) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Potty";
                        activitylog.StartTime = pottyRecord.ForthStampTime;
                        activitylog.ActivityDescription = pottyRecord.ForthDescription;
                        activitylog.CreatedDate = pottyRecord.ForthCreatedDate;
                        activitylog.CreatedBy = pottyRecord.ForthCreatedBy;
                        logs.Add(activitylog);
                    }
                }

                // 4-5) PottyRecord - fifth
                var pottyRecords5 = (from table in context.PottyRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate &&
                                     table.FifthStampTime != null
                                     select table).ToList();
                if(pottyRecords5.Count > 0) {
                    foreach(var pottyRecord in pottyRecords4) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "Potty";
                        activitylog.StartTime = pottyRecord.FifthStampTime;
                        activitylog.ActivityDescription = pottyRecord.FifthDescription;
                        activitylog.CreatedDate = pottyRecord.FifthCreatedDate;
                        activitylog.CreatedBy = pottyRecord.FifthCreatedBy;
                        logs.Add(activitylog);
                    }
                }





                return logs.OrderByDescending(x => x.CreatedDate);
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord getAttendanceOfTargetChild(AttendanceRecord model) {
            try {
                var result = (from table in context.AttendanceRecord
                              where table.ChildId == model.ChildId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.TargetDate == model.TargetDate
                              select table).FirstOrDefault();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord updateAttendanceRecordIn(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    model.InTime = model.InTime.Value.ToUniversalTime();
                    model.InTime_StampTime = model.InTime_StampTime.Value.ToUniversalTime();
                    model.CreatedDate = model.CreatedDate; // DateTime.UtcNow;
                    context.AttendanceRecord.Add(model);
                    context.SaveChanges();
                } else {
                    dbEntry.InTime = model.InTime.Value.ToUniversalTime();
                    dbEntry.InTime_StampTime = model.InTime_StampTime.Value.ToUniversalTime();
                    dbEntry.InTime_EnteredBy = model.InTime_EnteredBy;
                    dbEntry.UpdatedDate = model.CreatedDate;// DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return model;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord updateAttendanceRecordOut(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.OutTime = model.OutTime.Value.ToUniversalTime();
                    dbEntry.OutTime_StampTime = model.OutTime_StampTime.Value.ToUniversalTime();
                    dbEntry.OutTime_EnteredBy = model.OutTime_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate;// DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord updateAttendanceRecordTardy(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    model.Tardy_StampTime = model.Tardy_StampTime.Value.ToUniversalTime();
                    model.CreatedDate = model.CreatedDate;// DateTime.UtcNow;
                    context.AttendanceRecord.Add(model);
                    context.SaveChanges();
                } else {
                    dbEntry.Tardy = model.Tardy;
                    dbEntry.Tardy_StampTime = model.Tardy_StampTime.Value.ToUniversalTime();
                    dbEntry.Tardy_EnteredBy = model.Tardy_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate;// DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return model;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord updateAttendanceRecordAbsent(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    model.Absent_StampTime = model.Absent_StampTime.Value.ToUniversalTime();
                    model.CreatedDate = model.CreatedDate;// DateTime.UtcNow; 
                    context.AttendanceRecord.Add(model);
                    context.SaveChanges();
                } else {
                    dbEntry.Absent = model.Absent;
                    dbEntry.Absent_StampTime = model.Absent_StampTime.Value.ToUniversalTime();
                    dbEntry.Absent_EnteredBy = model.Absent_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate;// DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord updateAttendanceRecordLeaveEarly(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.LeaveEarly = model.LeaveEarly;
                    dbEntry.LeaveEarly_StampTime = model.LeaveEarly_StampTime.Value.ToUniversalTime();
                    dbEntry.LeaveEarly_EnteredBy = model.LeaveEarly_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord saveTardyComment(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.TardyComment = model.TardyComment;
                    dbEntry.Tardy_StampTime = model.Tardy_StampTime.Value.ToUniversalTime();
                    dbEntry.Tardy_EnteredBy = model.Tardy_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord saveAbsentComment(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.AbsentComment = model.AbsentComment;
                    dbEntry.Absent_StampTime = model.Absent_StampTime.Value.ToUniversalTime();
                    dbEntry.Absent_EnteredBy = model.Absent_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord saveLeaveEarlyComment(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.LeaveEarlyComment = model.LeaveEarlyComment;
                    dbEntry.LeaveEarly_StampTime = model.LeaveEarly_StampTime.Value.ToUniversalTime();
                    dbEntry.LeaveEarly_EnteredBy = model.LeaveEarly_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord cancelSignIn(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.InTime = null;
                    dbEntry.InTime_StampTime = null;
                    dbEntry.InTime_EnteredBy = null;
                    dbEntry.CancelInTime_StampTime = model.CancelInTime_StampTime;
                    dbEntry.CancelInTime_EnteredBy = model.CancelInTime_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public AttendanceRecord cancelSignOut(AttendanceRecord model) {
            try {
                var dbEntry = (from table in context.AttendanceRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry != null) {
                    dbEntry.OutTime = null;
                    dbEntry.OutTime_StampTime = null;
                    dbEntry.OutTime_EnteredBy = null;
                    dbEntry.CancelOutTime_StampTime = model.CancelOutTime_StampTime;
                    dbEntry.CancelOutTime_EnteredBy = model.CancelOutTime_EnteredBy;
                    dbEntry.UpdatedDate = model.UpdatedDate; //DateTime.UtcNow;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return dbEntry;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
