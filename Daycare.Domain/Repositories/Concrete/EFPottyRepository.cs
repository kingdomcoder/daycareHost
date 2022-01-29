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
    public class EFPottyRepository:IPottyRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFPottyRepository(MyDbContext context,MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public IEnumerable<PottyRecord> getTheirChildrenPottyRecordByOrganization(Organization model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join pottyRecord in context.PottyRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = pottyRecord.OrganizationId,
                                  ChildId = pottyRecord.ChildId ?? 0
                              } into PottyRecord_join
                              from pottyRecord_join in PottyRecord_join.DefaultIfEmpty()
                              where child.OrganizationId == model.OrganizationId &&
                                    (strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                    (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                    (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                    (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                    (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                    (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null)
                              select new PottyRecord() {
                                  PottyRecordId = pottyRecord_join.PottyRecordId,
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = pottyRecord_join.ImagePath,
                                  ImageFileName = pottyRecord_join.ImageFileName,
                                  TargetDate = child.TargetDate,
    
                                  PottyType = pottyRecord_join.PottyType,
                                  StampTime = pottyRecord_join.StampTime,
                                  Description = pottyRecord_join.Description,
                                  VisibleToStaffOnly = pottyRecord_join.VisibleToStaffOnly,
                                  CreatedDate = pottyRecord_join.CreatedDate,
                                  CreatedBy = pottyRecord_join.CreatedBy,

                                  FirstStampTime = pottyRecord_join.FirstStampTime,
                                  FirstDescription = pottyRecord_join.FirstDescription,
                                  FirstVisibleToStaffOnly = pottyRecord_join.FirstVisibleToStaffOnly,
                                  FirstCreatedDate = pottyRecord_join.FirstCreatedDate,
                                  FirstCreatedBy = pottyRecord_join.FirstCreatedBy,

                                  SecondStampTime = pottyRecord_join.SecondStampTime,
                                  SecondDescription = pottyRecord_join.SecondDescription,
                                  SecondVisibleToStaffOnly = pottyRecord_join.SecondVisibleToStaffOnly,
                                  SecondCreatedDate = pottyRecord_join.SecondCreatedDate,
                                  SecondCreatedBy = pottyRecord_join.SecondCreatedBy,

                                  ThirdStampTime = pottyRecord_join.ThirdStampTime,
                                  ThirdDescription = pottyRecord_join.ThirdDescription,
                                  ThirdVisibleToStaffOnly = pottyRecord_join.ThirdVisibleToStaffOnly,
                                  ThirdCreatedDate = pottyRecord_join.ThirdCreatedDate,
                                  ThirdCreatedBy = pottyRecord_join.ThirdCreatedBy,

                                  ForthStampTime = pottyRecord_join.ForthStampTime,
                                  ForthDescription = pottyRecord_join.ForthDescription,
                                  ForthVisibleToStaffOnly = pottyRecord_join.ForthVisibleToStaffOnly,
                                  ForthCreatedDate = pottyRecord_join.ForthCreatedDate,
                                  ForthCreatedBy = pottyRecord_join.ForthCreatedBy,

                                  FifthStampTime = pottyRecord_join.FifthStampTime,
                                  FifthDescription = pottyRecord_join.FifthDescription,
                                  FifthVisibleToStaffOnly = pottyRecord_join.FifthVisibleToStaffOnly,
                                  FifthCreatedDate = pottyRecord_join.FifthCreatedDate,
                                  FifthCreatedBy = pottyRecord_join.FifthCreatedBy,

                                  UpdatedDate = pottyRecord_join.UpdatedDate,
                                  UpdatedBy = pottyRecord_join.UpdatedBy
                              }).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PottyRecord> getTheirChildrenPottyRecordByChildOrganization(Child model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join pottyRecord in context.PottyRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = pottyRecord.OrganizationId,
                                  ChildId = pottyRecord.ChildId ?? 0
                              } into PottyRecord_join
                              from pottyRecord_join in PottyRecord_join.DefaultIfEmpty()
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

                              select new PottyRecord() {
                                  //PottyRecordId = pottyRecord_join.PottyRecordId, //Need to delete becuae it should not be null
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
                             select new PottyRecord() {
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
                    var result2 = (from napRecord in context.PottyRecord
                                   where
                                   napRecord.OrganizationId == obj.OrganizationId &&
                                   napRecord.ChildId == obj.ChildId &&
                                   napRecord.TargetDate == model.TargetDate
                                   select napRecord).FirstOrDefault();
                    if(result2 != null) {
                        obj.PottyType = result2.PottyType;
                        obj.StampTime = result2.StampTime;
                        obj.Description = result2.Description;
                        obj.VisibleToStaffOnly = result2.VisibleToStaffOnly;
                        obj.CreatedDate = result2.CreatedDate;
                        obj.CreatedBy = result2.CreatedBy;

                        obj.FirstStampTime = result2.FirstStampTime;
                        obj.FirstDescription = result2.FirstDescription;
                        obj.FirstVisibleToStaffOnly = result2.FirstVisibleToStaffOnly;
                        obj.FirstCreatedDate = result2.FirstCreatedDate;
                        obj.FirstCreatedBy = result2.FirstCreatedBy;

                        obj.SecondStampTime = result2.SecondStampTime;
                        obj.SecondDescription = result2.SecondDescription;
                        obj.SecondVisibleToStaffOnly = result2.SecondVisibleToStaffOnly;
                        obj.SecondCreatedDate = result2.SecondCreatedDate;
                        obj.SecondCreatedBy = result2.SecondCreatedBy;

                        obj.ThirdStampTime = result2.ThirdStampTime;
                        obj.ThirdDescription = result2.ThirdDescription;
                        obj.ThirdVisibleToStaffOnly = result2.ThirdVisibleToStaffOnly;
                        obj.ThirdCreatedDate = result2.ThirdCreatedDate;
                        obj.ThirdCreatedBy = result2.ThirdCreatedBy;

                        obj.ForthStampTime = result2.ForthStampTime;
                        obj.ForthDescription = result2.ForthDescription;
                        obj.ForthVisibleToStaffOnly = result2.ForthVisibleToStaffOnly;
                        obj.ForthCreatedDate = result2.ForthCreatedDate;
                        obj.ForthCreatedBy = result2.ForthCreatedBy;

                        obj.FifthStampTime = result2.FifthStampTime;
                        obj.FifthDescription = result2.FifthDescription;
                        obj.FifthVisibleToStaffOnly = result2.FifthVisibleToStaffOnly;
                        obj.FifthCreatedDate = result2.FifthCreatedDate;
                        obj.FifthCreatedBy = result2.FifthCreatedBy;

                        obj.UpdatedDate = result2.UpdatedDate;
                        obj.UpdatedBy = result2.UpdatedBy;
                              }
                }
                return query;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public PottyRecord getPottyOfTargetChild(PottyRecord model) {
            try {
                var result = (from table in context.PottyRecord
                              where table.ChildId == model.ChildId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.TargetDate == model.TargetDate
                              select table).FirstOrDefault();
                if(result!=null) {
                    switch(model.PottyType) {
                        case "First":
                            result.PottyType = "First";
                            result.StampTime = result.FirstStampTime;
                            result.Description = result.FirstDescription;
                            result.VisibleToStaffOnly = result.FirstVisibleToStaffOnly;
                            result.CreatedDate = result.FirstCreatedDate;
                            result.CreatedBy = result.FirstCreatedBy;
                            break;
                        case "Second":
                            result.PottyType = "Second";
                            result.StampTime = result.SecondStampTime;
                            result.Description = result.SecondDescription;
                            result.VisibleToStaffOnly = result.SecondVisibleToStaffOnly;
                            result.CreatedDate = result.SecondCreatedDate;
                            result.CreatedBy = result.SecondCreatedBy;
                            break;
                        case "Third":
                            result.PottyType = "Third";
                            result.StampTime = result.ThirdStampTime;
                            result.Description = result.ThirdDescription;
                            result.VisibleToStaffOnly = result.ThirdVisibleToStaffOnly;
                            result.CreatedDate = result.ThirdCreatedDate;
                            result.CreatedBy = result.ThirdCreatedBy;
                            break;
                        case "Forth":
                            result.PottyType = "Forth";
                            result.StampTime = result.ForthStampTime;
                            result.Description = result.ForthDescription;
                            result.VisibleToStaffOnly = result.ForthVisibleToStaffOnly;
                            result.CreatedDate = result.ForthCreatedDate;
                            result.CreatedBy = result.ForthCreatedBy;
                            break;
                        case "Fifth":
                            result.PottyType = "Fifth";
                            result.StampTime = result.FifthStampTime;
                            result.Description = result.FifthDescription;
                            result.VisibleToStaffOnly = result.FifthVisibleToStaffOnly;
                            result.CreatedDate = result.FifthCreatedDate;
                            result.CreatedBy = result.FifthCreatedBy;
                            break;
                    }
                }

                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public PottyRecord savePottyRecord(PottyRecord model) {
            try {
                var dbEntry = (from table in context.PottyRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    model.FirstStampTime = model.StampTime;
                    model.FirstDescription = model.Description;
                    model.FirstVisibleToStaffOnly = model.VisibleToStaffOnly;
                    model.FirstCreatedDate = model.CreatedDate;
                    model.FirstCreatedBy = model.FirstCreatedBy;
                    model.StampTime = null;
                    model.Description = null;
                    model.VisibleToStaffOnly = null;
                    model.CreatedDate = null;
                    model.CreatedBy = null;

                    context.PottyRecord.Add(model);
                    context.SaveChanges();
                } else {
                    if(dbEntry.SecondStampTime==null) {
                        dbEntry.SecondStampTime = model.StampTime;
                        dbEntry.SecondDescription = model.Description;
                        dbEntry.SecondVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.SecondCreatedDate = model.CreatedDate;
                        dbEntry.SecondCreatedBy = model.CreatedBy;
                    } else if(dbEntry.ThirdStampTime == null) {
                        dbEntry.ThirdStampTime = model.StampTime;
                        dbEntry.ThirdDescription = model.Description;
                        dbEntry.ThirdVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.ThirdCreatedDate = model.CreatedDate;
                        dbEntry.ThirdCreatedBy = model.CreatedBy;
                    }
                    else if(dbEntry.ForthStampTime == null) {
                        dbEntry.ForthStampTime = model.StampTime;
                        dbEntry.ForthDescription = model.Description;
                        dbEntry.ForthVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.ForthCreatedDate = model.CreatedDate;
                        dbEntry.ForthCreatedBy = model.CreatedBy;
                    }
                    else if(dbEntry.FifthStampTime == null) {
                        dbEntry.FifthStampTime = model.StampTime;
                        dbEntry.FifthDescription = model.Description;
                        dbEntry.FifthVisibleToStaffOnly = model.VisibleToStaffOnly;
                        dbEntry.FifthCreatedDate = model.CreatedDate;
                        dbEntry.FifthCreatedBy = model.CreatedBy;
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
