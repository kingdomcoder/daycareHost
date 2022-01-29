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
    public class EFNapRepository:INapRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFNapRepository(MyDbContext context,MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public IEnumerable<NapRecord> getTheirChildrenNapRecordByOrganization(Organization model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join napRecord in context.NapRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = napRecord.OrganizationId,
                                  ChildId = napRecord.ChildId ?? 0
                              } into NapRecord_join
                              from napRecord_join in NapRecord_join.DefaultIfEmpty()
                              where child.OrganizationId == model.OrganizationId &&
                                    (strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                    (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                    (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                    (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                    (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                    (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null)
                              select new NapRecord() {
                                  NapRecordId = napRecord_join.NapRecordId,
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = napRecord_join.ImagePath,
                                  ImageFileName = napRecord_join.ImageFileName,
                                  TargetDate = child.TargetDate,
                                  EndStampTime = napRecord_join.EndStampTime,
                                  NapType = napRecord_join.NapType,
                                  Description = napRecord_join.Description,
                                  VisibleToStaffOnly = napRecord_join.VisibleToStaffOnly,
                                  CreatedDate = napRecord_join.CreatedDate,
                                  CreatedBy = napRecord_join.CreatedBy,

                                  FirstStartStampTime = napRecord_join.FirstStartStampTime,
                                  FirstEndStampTime = napRecord_join.FirstEndStampTime,
                                  FirstDescription = napRecord_join.FirstDescription,
                                  FirstVisibleToStaffOnly = napRecord_join.FirstVisibleToStaffOnly,
                                  FirstCreatedDate = napRecord_join.FirstCreatedDate,
                                  FirstCreatedBy = napRecord_join.FirstCreatedBy,

                                  SecondStartStampTime = napRecord_join.SecondStartStampTime,
                                  SecondEndStampTime = napRecord_join.SecondEndStampTime,
                                  SecondDescription = napRecord_join.SecondDescription,
                                  SecondVisibleToStaffOnly = napRecord_join.SecondVisibleToStaffOnly,
                                  SecondCreatedDate = napRecord_join.SecondCreatedDate,
                                  SecondCreatedBy = napRecord_join.SecondCreatedBy,

                                  ThirdStartStampTime = napRecord_join.ThirdStartStampTime,
                                  ThirdEndStampTime = napRecord_join.ThirdEndStampTime,
                                  ThirdDescription = napRecord_join.ThirdDescription,
                                  ThirdVisibleToStaffOnly = napRecord_join.ThirdVisibleToStaffOnly,
                                  ThirdCreatedDate = napRecord_join.ThirdCreatedDate,
                                  ThirdCreatedBy = napRecord_join.ThirdCreatedBy,

                                  UpdatedDate = napRecord_join.UpdatedDate,
                                  UpdatedBy = napRecord_join.UpdatedBy
                              }).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NapRecord> getTheirChildrenNapRecordByChildOrganization(Child model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join napRecord in context.NapRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = napRecord.OrganizationId,
                                  ChildId = napRecord.ChildId ?? 0
                              } into NapRecord_join
                              from napRecord_join in NapRecord_join.DefaultIfEmpty()
                              where
                              (child.OrganizationId == model.OrganizationId) &&
                              ((model.ChildId == null || model.ChildId == 0) ? null == null : child.ChildId == model.ChildId) &&
                               child.ActiveStatus == true
                              &&
                                    (strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                    (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                    (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                    (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                    (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                    (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null)
                              select new NapRecord() {
                                //NapRecordId = napRecord_join.AttendanceRecordId, //Need to delete becuae it should not be null
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
                                             select new NapRecord() {
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
                    var result2 = (from napRecord in context.NapRecord
                                   where
                                   napRecord.OrganizationId == obj.OrganizationId &&
                                   napRecord.ChildId == obj.ChildId &&
                                   napRecord.TargetDate == model.TargetDate
                                   select napRecord).FirstOrDefault();
                    if(result2 != null) {

                        obj.EndStampTime = result2.EndStampTime;
                        obj.NapType = result2.NapType;
                        obj.Description = result2.Description;
                        obj.VisibleToStaffOnly = result2.VisibleToStaffOnly;
                        obj.CreatedDate = result2.CreatedDate;
                        obj.CreatedBy = result2.CreatedBy;

                        obj.FirstStartStampTime = result2.FirstStartStampTime;
                        obj.FirstEndStampTime = result2.FirstEndStampTime;
                        obj.FirstDescription = result2.FirstDescription;
                        obj.FirstVisibleToStaffOnly = result2.FirstVisibleToStaffOnly;
                        obj.FirstCreatedDate = result2.FirstCreatedDate;
                        obj.FirstCreatedBy = result2.FirstCreatedBy;

                        obj.SecondStartStampTime = result2.SecondStartStampTime;
                        obj.SecondEndStampTime = result2.SecondEndStampTime;
                        obj.SecondDescription = result2.SecondDescription;
                        obj.SecondVisibleToStaffOnly = result2.SecondVisibleToStaffOnly;
                        obj.SecondCreatedDate = result2.SecondCreatedDate;
                        obj.SecondCreatedBy = result2.SecondCreatedBy;

                        obj.ThirdStartStampTime = result2.ThirdStartStampTime;
                        obj.ThirdEndStampTime = result2.ThirdEndStampTime;
                        obj.ThirdDescription = result2.ThirdDescription;
                        obj.ThirdVisibleToStaffOnly = result2.ThirdVisibleToStaffOnly;
                        obj.ThirdCreatedDate = result2.ThirdCreatedDate;
                        obj.ThirdCreatedBy = result2.ThirdCreatedBy;

                        obj.UpdatedDate = result2.UpdatedDate;
                    }
                }
                return query;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public NapRecord getNapOfTargetChild(NapRecord model) {
            try {
                var result = (from table in context.NapRecord
                              where table.ChildId == model.ChildId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.TargetDate == model.TargetDate
                              select table).FirstOrDefault();
                if(result !=null) {
                    switch(model.NapType) {
                        case "First":
                            result.NapType = "First";
                            result.StartStampTime = result.FirstStartStampTime;
                            result.EndStampTime = result.FirstEndStampTime;
                            result.Description = result.FirstDescription;
                            result.VisibleToStaffOnly = result.FirstVisibleToStaffOnly;
                            result.CreatedDate = result.FirstCreatedDate;
                            result.CreatedBy = result.FirstCreatedBy;
                            break;
                        case "Second":
                            result.NapType = "Second";
                            result.StartStampTime = result.SecondStartStampTime;
                            result.EndStampTime = result.SecondEndStampTime;
                            result.Description = result.SecondDescription;
                            result.VisibleToStaffOnly = result.SecondVisibleToStaffOnly;
                            result.CreatedDate = result.SecondCreatedDate;
                            result.CreatedBy = result.SecondCreatedBy;
                            break;
                        case "Third":
                            result.NapType = "Third";
                            result.StartStampTime = result.ThirdStartStampTime;
                            result.EndStampTime = result.ThirdEndStampTime;
                            result.Description = result.ThirdDescription;
                            result.VisibleToStaffOnly = result.ThirdVisibleToStaffOnly;
                            result.CreatedDate = result.ThirdCreatedDate;
                            result.CreatedBy = result.ThirdCreatedBy;
                            break;
                    }
                }

                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public NapRecord saveNapRecord(NapRecord model) {
            try {
                var dbEntry = (from table in context.NapRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    model.FirstStartStampTime = model.StartStampTime;
                    model.FirstEndStampTime = model.EndStampTime;
                    model.FirstDescription = model.Description;
                    model.FirstVisibleToStaffOnly = model.VisibleToStaffOnly;
                    model.FirstCreatedDate = model.CreatedDate;
                    model.FirstCreatedBy = model.FirstCreatedBy;
                    model.StartStampTime = null;
                    model.Description = null;
                    model.VisibleToStaffOnly = null;
                    model.CreatedDate = null;
                    model.CreatedBy = null;

                    context.NapRecord.Add(model);
                    context.SaveChanges();
                } else {
                    if(dbEntry.SecondStartStampTime==null) {
                        dbEntry.SecondStartStampTime = model.StartStampTime;
                        dbEntry.SecondEndStampTime = model.EndStampTime;
                        dbEntry.SecondDescription = model.Description;
                        dbEntry.SecondVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.SecondCreatedDate = model.CreatedDate;
                        dbEntry.SecondCreatedBy = model.CreatedBy;
                    }
                    else if(dbEntry.ThirdStartStampTime == null) {
                        dbEntry.ThirdStartStampTime = model.StartStampTime;
                        dbEntry.ThirdEndStampTime = model.EndStampTime;
                        dbEntry.ThirdDescription = model.Description;
                        dbEntry.ThirdVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.ThirdCreatedDate = model.CreatedDate;
                        dbEntry.ThirdCreatedBy = model.CreatedBy;
                    }
                    dbEntry.UpdatedDate = DateTime.UtcNow;
                    dbEntry.UpdatedBy = model.UpdatedBy;
                    context.Entry(dbEntry).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return model;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

    }
}
