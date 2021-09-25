using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Daycare.Domain.Entities;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFChildRepository : IChildRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFChildRepository(MyDbContext context, MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public void CreateChild(Child model) {
            try {
                Child dbEntry = (from table in context.Child
                                 where
                                 table.ChildId == model.ChildId
                                 select table
                                  ).FirstOrDefault();

                if (dbEntry == null || model.ChildId == null) {
                    /********************************************************************************
                        * This adjustment is because wherever user is, default time for insert is 8:00am.
                        * Need to add 4hours so that UTC becomes 12:00pm. 12:00pm is best because wherever
                        * user is, it returns same date.
                        * *******************************************************************************/
                    model.RegisteredDate =DateTime.Parse(model.RegisteredDate.ToString()).AddHours(4);
                    context.Child.Add(model);
                    context.SaveChanges();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateChild(Child model) {
            try {
                Child dbEntry = (from table in context.Child
                                 where
                                 table.ChildId == model.ChildId
                                 select table
                                  ).FirstOrDefault();

                if (dbEntry != null || model.ChildId != null) {
                    dbEntry.Parent1Id = model.Parent1Id;
                    dbEntry.Parent2Id = model.Parent2Id;
                    dbEntry.OrganizationId = model.OrganizationId;
                    dbEntry.ChildFirstName = model.ChildFirstName;
                    dbEntry.ChildLastName = model.ChildLastName;
                    dbEntry.ChildMiddleName = model.ChildMiddleName;
                    dbEntry.ChildShimei = model.ChildShimei;
                    dbEntry.ChildMyoji = model.ChildMyoji;
                    dbEntry.Gender = model.Gender;
                    dbEntry.DOB = model.DOB;
                    dbEntry.Grade = model.Grade;
                    dbEntry.ClassName = model.ClassName;
                    dbEntry.AttendMon = model.AttendMon;
                    dbEntry.AttendTue = model.AttendTue;
                    dbEntry.AttendWed = model.AttendWed;
                    dbEntry.AttendThu = model.AttendThu;
                    dbEntry.AttendFri = model.AttendFri;
                    dbEntry.AttendSat = model.AttendSat;
                    dbEntry.AttendSun = model.AttendSun;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Activity> getActivityByChild(Child model) {
            try {
                List<Activity> logs = new List<Activity>();

                // 1) AttendanceRecord
                var attendancelogs = (from table in context.AttendanceRecord
                                     where table.ChildId == model.ChildId &&
                                     table.OrganizationId == model.OrganizationId &&
                                     table.TargetDate == model.TargetDate
                                     select table).ToList();
                if (attendancelogs.Count > 0) {
                    foreach (var attendancelog in attendancelogs) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "AttendanceRecord";
                        activitylog.StartTime = attendancelog.InTime;
                        activitylog.EndTime = attendancelog.OutTime;
                        activitylog.ActivityDescription = attendancelog.Memo;
                        activitylog.CreatedDate = attendancelog.RecordedDate;
                        logs.Add(activitylog);
                    }
                }
                // 2) MealRecord
                var mealRecords = (from table in context.MealRecord
                                      where table.ChildId == model.ChildId &&
                                      table.TargetDate == model.TargetDate
                                   select table).ToList();
                if (mealRecords.Count > 0) {
                    foreach (var mealRecord in mealRecords) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "MealRecord";
                        activitylog.StartTime = mealRecord.ActivityStartTime;
                        activitylog.EndTime = mealRecord.ActivityEndTime;
                        activitylog.ActivityDescription = mealRecord.Memo;
                        activitylog.CreatedDate = mealRecord.RecordedDate;
                        logs.Add(activitylog);
                    }
                }
                // 3) NapRecord
                var napRecords = (from table in context.NapRecord
                                  where table.ChildId == model.ChildId &&
                                  table.TargetDate == model.TargetDate
                                  select table).ToList();
                if (mealRecords.Count > 0) {
                    foreach (var napRecord in napRecords) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "NapRecord";
                        activitylog.StartTime = napRecord.ActivityStartTime;
                        activitylog.EndTime = napRecord.ActivityEndTime;
                        activitylog.ActivityDescription = napRecord.Memo;
                        activitylog.CreatedDate = napRecord.RecordedDate;
                        logs.Add(activitylog);
                    }
                }
                // 4) PottyRecord
                var pottyRecords = (from table in context.PottyRecord
                                    where table.ChildId == model.ChildId &&
                                    table.TargetDate == model.TargetDate
                                    select table).ToList();
                if (mealRecords.Count > 0) {
                    foreach (var pottyRecord in pottyRecords) {
                        Activity activitylog = new Activity();
                        activitylog.ChildId = model.ChildId;
                        activitylog.ActivityName = "PottyRecord";
                        activitylog.StartTime = pottyRecord.ActivityStartTime;
                        activitylog.EndTime = pottyRecord.ActivityEndTime;
                        activitylog.ActivityDescription = pottyRecord.Memo;
                        activitylog.CreatedDate = pottyRecord.RecordedDate;
                        logs.Add(activitylog);
                    }
                }

                return logs.OrderByDescending(x => x.CreatedDate);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Child> GetMyChildrenByParentId(string id) {
            try {
                var children = (from table in context.Child
                                where table.Parent1Id == id ||
                                table.Parent2Id == id
                                select table).ToList();
                return children;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Child> getTheirChildrenByOrganization(Organization model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if (model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");


                }
                // 2. Check absent notice 


                var children = (from table in context.Child
                                where table.OrganizationId == model.OrganizationId &&
                                (strDayOfWeek == "Mon" ? table.AttendMon == true : null == null) &&
                                (strDayOfWeek == "Tue" ? table.AttendTue == true : null == null) &&
                                 (strDayOfWeek == "Wed" ? table.AttendWed == true : null == null) &&
                                  (strDayOfWeek == "Thu" ? table.AttendThu == true : null == null) &&
                                   (strDayOfWeek == "Fri" ? table.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? table.AttendSat == true : null == null) &&
                                     (strDayOfWeek == "Sun" ? table.AttendSun == true : null == null)
                                select table).ToList();
                return children;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Child> GetMyChildrenByParentUser(ApplicationUser model) {
            try {
                var children = (from table in context.Child
                                where (table.Parent1Id == model.Id  ||
                                table.Parent2Id == model.Id) &&
                                table.OrganizationId == model.OrganizationId
                                select table).ToList();
                return children;
            } catch (Exception ex) {
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

                if (dbEntry == null || model.ChildId == null) {
                    context.CommentRecord.Add(model);
                    context.SaveChanges();
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void SendErrorMessageToAdmin(Child model, string message) {
            try {

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public Child GetChildByChildId(int  id) {
            try {
                var child = (from table in context.Child
                                where table.ChildId == id 
                                select table).FirstOrDefault();
                return child;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
