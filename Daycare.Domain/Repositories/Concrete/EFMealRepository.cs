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
    public class EFMealRepository:IMealRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFMealRepository(MyDbContext context,MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public IEnumerable<MealRecord> getTheirChildrenMealRecordByOrganization(Organization model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join mealRecord in context.MealRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = mealRecord.OrganizationId,
                                  ChildId = mealRecord.ChildId ?? 0
                              } into MealRecord_join
                              from mealRecord_join in MealRecord_join.DefaultIfEmpty()
                              where child.OrganizationId == model.OrganizationId &&
                                    (strDayOfWeek == "Mon" ? child.AttendMon == true : null == null) &&
                                    (strDayOfWeek == "Tue" ? child.AttendTue == true : null == null) &&
                                    (strDayOfWeek == "Wed" ? child.AttendWed == true : null == null) &&
                                    (strDayOfWeek == "Thu" ? child.AttendThu == true : null == null) &&
                                    (strDayOfWeek == "Fri" ? child.AttendFri == true : null == null) &&
                                    (strDayOfWeek == "Sat" ? child.AttendSat == true : null == null) &&
                                    (strDayOfWeek == "Sun" ? child.AttendSun == true : null == null)
                              select new MealRecord() {
                                  MealRecordId = mealRecord_join.MealRecordId,
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = mealRecord_join.ImagePath,
                                  ImageFileName = mealRecord_join.ImageFileName,
                                  TargetDate = child.TargetDate,
                                  EndStampTime = mealRecord_join.EndStampTime,
                                  MealType = mealRecord_join.MealType,
                                  Quantity = mealRecord_join.Quantity,
                                  Description = mealRecord_join.Description,
                                  VisibleToStaffOnly = mealRecord_join.VisibleToStaffOnly,
                                  CreatedDate=mealRecord_join.CreatedDate,
                                  CreatedBy=mealRecord_join.CreatedBy,

                                  Breakfast = mealRecord_join.Breakfast,
                                  BreakfastQuantity=mealRecord_join.BreakfastQuantity,
                                  BreakfastDescription=mealRecord_join.BreakfastDescription,
                                  BreakfastVisibleToStaffOnly=mealRecord_join.BreakfastVisibleToStaffOnly,
                                  BreakfastCreatedDate=mealRecord_join.BreakfastCreatedDate,
                                  BreakfastCreatedBy=mealRecord_join.BreakfastCreatedBy,

                                  AMSnack=mealRecord_join.AMSnack,
                                  AMSnackQuantity=mealRecord_join.AMSnackQuantity,
                                  AMSnackDescription=mealRecord_join.AMSnackDescription,
                                  AMSnackVisibleToStaffOnly=mealRecord_join.AMSnackVisibleToStaffOnly,
                                  AMSnackCreatedDate=mealRecord_join.AMSnackCreatedDate,
                                  AMSnackCreatedBy=mealRecord_join.AMSnackCreatedBy,

                                  Lunch = mealRecord_join.Lunch,
                                  LunchQuantity = mealRecord_join.LunchQuantity,
                                  LunchDescription = mealRecord_join.LunchDescription,
                                  LunchVisibleToStaffOnly = mealRecord_join.LunchVisibleToStaffOnly,
                                  LunchCreatedDate = mealRecord_join.LunchCreatedDate,
                                  LunchCreatedBy = mealRecord_join.LunchCreatedBy,

                                  PMSnack = mealRecord_join.PMSnack,
                                  PMSnackQuantity = mealRecord_join.PMSnackQuantity,
                                  PMSnackDescription = mealRecord_join.PMSnackDescription,
                                  PMSnackVisibleToStaffOnly = mealRecord_join.PMSnackVisibleToStaffOnly,
                                  PMSnackCreatedDate = mealRecord_join.PMSnackCreatedDate,
                                  PMSnackCreatedBy = mealRecord_join.PMSnackCreatedBy,

                                  LateSnack = mealRecord_join.LateSnack,
                                  LateSnackQuantity = mealRecord_join.LateSnackQuantity,
                                  LateSnackDescription = mealRecord_join.LateSnackDescription,
                                  LateSnackVisibleToStaffOnly = mealRecord_join.LateSnackVisibleToStaffOnly,
                                  LateSnackCreatedDate = mealRecord_join.LateSnackCreatedDate,
                                  LateSnackCreatedBy = mealRecord_join.LateSnackCreatedBy,

                                  Dinner = mealRecord_join.Dinner,
                                  DinnerQuantity = mealRecord_join.DinnerQuantity,
                                  DinnerDescription = mealRecord_join.DinnerDescription,
                                  DinnerVisibleToStaffOnly = mealRecord_join.DinnerVisibleToStaffOnly,
                                  DinnerCreatedDate = mealRecord_join.DinnerCreatedDate,
                                  DinnerCreatedBy = mealRecord_join.DinnerCreatedBy,

                                  UpdatedDate =mealRecord_join.UpdatedDate,
                                  UpdatedBy=mealRecord_join.UpdatedBy
                              }).ToList();
                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<MealRecord> getTheirChildrenMealRecordByChildOrganization(Child model) {
            try {
                String strDayOfWeek = string.Empty;
                // 1. Check day of the week for the target date
                if(model.TargetDate != null) {
                    DateTime dateValue = DateTime.Parse(model.TargetDate.Value.ToString());
                    strDayOfWeek = dateValue.ToString("ddd");
                }
                var result = (from child in context.Child
                              join mealRecord in context.MealRecord
                              on new {
                                  OrganizationId = child.OrganizationId,
                                  ChildId = child.ChildId
                              } equals new {
                                  OrganizationId = mealRecord.OrganizationId,
                                  ChildId = mealRecord.ChildId ?? 0
                              } into MealRecord_join
                              from mealRecord_join in MealRecord_join.DefaultIfEmpty()
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

                              select new MealRecord() {
                                  //    MealRecordId = mealRecord_join.MealRecordId,
                                  ChildId = child.ChildId,
                                  OrganizationId = child.OrganizationId,
                                  Parent1Id = child.Parent1Id,
                                  Parent2Id = child.Parent2Id,
                                  ChildFirstName = child.ChildFirstName,
                                  ChildLastName = child.ChildLastName,
                                  ImagePath = child.ImagePath,
                                  ImageFileName = child.ImageFileName
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
                             select new MealRecord() {
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
                    var result2 = (from attendanceRecord in context.MealRecord
                                   where
                                   attendanceRecord.OrganizationId == obj.OrganizationId &&
                                   attendanceRecord.ChildId == obj.ChildId &&
                                   attendanceRecord.TargetDate == model.TargetDate
                                   select attendanceRecord).FirstOrDefault();
                    if(result2 != null) {
                        obj.EndStampTime = result2.EndStampTime;
                        obj.MealType = result2.MealType;
                        obj.Quantity = result2.Quantity;
                        obj.Description = result2.Description;
                        obj.VisibleToStaffOnly = result2.VisibleToStaffOnly;
                        obj.CreatedDate = result2.CreatedDate;
                        obj.CreatedBy = result2.CreatedBy;

                        obj.Breakfast = result2.Breakfast;
                        obj.BreakfastQuantity = result2.BreakfastQuantity;
                        obj.BreakfastDescription = result2.BreakfastDescription;
                        obj.BreakfastVisibleToStaffOnly = result2.BreakfastVisibleToStaffOnly;
                        obj.BreakfastCreatedDate = result2.BreakfastCreatedDate;
                        obj.BreakfastCreatedBy = result2.BreakfastCreatedBy;

                        obj.AMSnack = result2.AMSnack;
                        obj.AMSnackQuantity = result2.AMSnackQuantity;
                        obj.AMSnackDescription = result2.AMSnackDescription;
                        obj.AMSnackVisibleToStaffOnly = result2.AMSnackVisibleToStaffOnly;
                        obj.AMSnackCreatedDate = result2.AMSnackCreatedDate;
                        obj.AMSnackCreatedBy = result2.AMSnackCreatedBy;

                        obj.Lunch = result2.Lunch;
                        obj.LunchQuantity = result2.LunchQuantity;
                        obj.LunchDescription = result2.LunchDescription;
                        obj.LunchVisibleToStaffOnly = result2.LunchVisibleToStaffOnly;
                        obj.LunchCreatedDate = result2.LunchCreatedDate;
                        obj.LunchCreatedBy = result2.LunchCreatedBy;

                        obj.PMSnack = result2.PMSnack;
                        obj.PMSnackQuantity = result2.PMSnackQuantity;
                        obj.PMSnackDescription = result2.PMSnackDescription;
                        obj.PMSnackVisibleToStaffOnly = result2.PMSnackVisibleToStaffOnly;
                        obj.PMSnackCreatedDate = result2.PMSnackCreatedDate;
                        obj.PMSnackCreatedBy = result2.PMSnackCreatedBy;

                        obj.LateSnack = result2.LateSnack;
                        obj.LateSnackQuantity = result2.LateSnackQuantity;
                        obj.LateSnackDescription = result2.LateSnackDescription;
                        obj.LateSnackVisibleToStaffOnly = result2.LateSnackVisibleToStaffOnly;
                        obj.LateSnackCreatedDate = result2.LateSnackCreatedDate;
                        obj.LateSnackCreatedBy = result2.LateSnackCreatedBy;

                        obj.Dinner = result2.Dinner;
                        obj.DinnerQuantity = result2.DinnerQuantity;
                        obj.DinnerDescription = result2.DinnerDescription;
                        obj.DinnerVisibleToStaffOnly = result2.DinnerVisibleToStaffOnly;
                        obj.DinnerCreatedDate = result2.DinnerCreatedDate;
                        obj.DinnerCreatedBy = result2.DinnerCreatedBy;

                        obj.UpdatedDate = result2.UpdatedDate;
                        obj.UpdatedBy = result2.UpdatedBy;
                    }
                }
                return query;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public MealRecord getMealOfTargetChild(MealRecord model) {
            try {
                var result = (from table in context.MealRecord
                              where table.ChildId == model.ChildId &&
                              table.OrganizationId == model.OrganizationId &&
                              table.TargetDate == model.TargetDate
                              select table).FirstOrDefault();

                switch(model.MealType) {
                    case "Breakfast":
                        result.MealType = "Breakfast";
                        result.Quantity = result.BreakfastQuantity;
                        result.Description = result.BreakfastDescription;
                        result.VisibleToStaffOnly = result.BreakfastVisibleToStaffOnly;
                        result.CreatedDate = result.BreakfastCreatedDate;
                        result.CreatedBy = result.BreakfastCreatedBy;
                        break;
                    case "AM Snack":
                        result.MealType = "AM Snack";
                        result.Quantity = result.AMSnackQuantity;
                        result.Description = result.AMSnackDescription;
                        result.VisibleToStaffOnly = result.AMSnackVisibleToStaffOnly;
                        result.CreatedDate = result.AMSnackCreatedDate;
                        result.CreatedBy = result.AMSnackCreatedBy;
                        break;
                    case "Lunch":
                        result.MealType = "Lunch";
                        result.Quantity = result.LunchQuantity;
                        result.Description = result.LunchDescription;
                        result.VisibleToStaffOnly = result.LunchVisibleToStaffOnly;
                        result.CreatedDate = result.LunchCreatedDate;
                        result.CreatedBy = result.LunchCreatedBy;
                        break;
                    case "PM Snack":
                        result.MealType = "PM Snack";
                        result.Quantity = result.PMSnackQuantity;
                        result.Description = result.PMSnackDescription;
                        result.VisibleToStaffOnly = result.PMSnackVisibleToStaffOnly;
                        result.CreatedDate = result.PMSnackCreatedDate;
                        result.CreatedBy = result.PMSnackCreatedBy;
                        break;
                    case "Late Snack":
                        result.MealType = "Late Snack";
                        result.Quantity = result.LateSnackQuantity;
                        result.Description = result.LateSnackDescription;
                        result.VisibleToStaffOnly = result.LateSnackVisibleToStaffOnly;
                        result.CreatedDate = result.LateSnackCreatedDate;
                        result.CreatedBy = result.LateSnackCreatedBy;
                        break;
                    case "Dinner":
                        result.MealType = "Dinner";
                        result.Quantity = result.DinnerQuantity;
                        result.Description = result.DinnerDescription;
                        result.VisibleToStaffOnly = result.DinnerVisibleToStaffOnly;
                        result.CreatedDate = result.DinnerCreatedDate;
                        result.CreatedBy = result.DinnerCreatedBy;
                        break;
                }



                return result;
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public MealRecord saveMealRecord(MealRecord model) {
            try {
                var dbEntry = (from table in context.MealRecord
                               where table.ChildId == model.ChildId &&
                               table.OrganizationId == model.OrganizationId &&
                               table.TargetDate == model.TargetDate 
                               select table).FirstOrDefault();
                if(dbEntry == null) {
                    context.MealRecord.Add(model);
                    context.SaveChanges();
                } else {


                    //dbEntry.Breakfast = model.Breakfast;
                    //dbEntry.BreakfastQuantity = model.BreakfastQuantity;
                    //dbEntry.BreakfastDescription = model.BreakfastDescription;
                    //dbEntry.BreakfastVisibleToStaffOnly = model.BreakfastVisibleToStaffOnly;
                    //dbEntry.BreakfastCreatedDate = model.BreakfastCreatedDate;
                    //dbEntry.BreakfastCreatedBy = model.BreakfastCreatedBy;

                    //dbEntry.AMSnack = model.AMSnack;
                    //dbEntry.AMSnackQuantity = model.AMSnackQuantity;
                    //dbEntry.AMSnackDescription = model.AMSnackDescription;
                    //dbEntry.AMSnackVisibleToStaffOnly = model.AMSnackVisibleToStaffOnly;
                    //dbEntry.AMSnackCreatedDate = model.AMSnackCreatedDate;
                    //dbEntry.AMSnackCreatedBy = model.AMSnackCreatedBy;

                    //dbEntry.Lunch = model.Lunch;
                    //dbEntry.LunchQuantity = model.LunchQuantity;
                    //dbEntry.LunchDescription = model.LunchDescription;
                    //dbEntry.LunchVisibleToStaffOnly = model.LunchVisibleToStaffOnly;
                    //dbEntry.LunchCreatedDate = model.LunchCreatedDate;
                    //dbEntry.LunchCreatedBy = model.LunchCreatedBy;

                    //dbEntry.PMSnack = model.PMSnack;
                    //dbEntry.PMSnackQuantity = model.PMSnackQuantity;
                    //dbEntry.PMSnackDescription = model.PMSnackDescription;
                    //dbEntry.PMSnackVisibleToStaffOnly = model.PMSnackVisibleToStaffOnly;
                    //dbEntry.PMSnackCreatedDate = model.PMSnackCreatedDate;
                    //dbEntry.PMSnackCreatedBy = model.PMSnackCreatedBy;

                    //dbEntry.LateSnack = model.LateSnack;
                    //dbEntry.LateSnackQuantity = model.LateSnackQuantity;
                    //dbEntry.LateSnackDescription = model.LateSnackDescription;
                    //dbEntry.LateSnackVisibleToStaffOnly = model.LateSnackVisibleToStaffOnly;
                    //dbEntry.LateSnackCreatedDate = model.LateSnackCreatedDate;
                    //dbEntry.LateSnackCreatedBy = model.LateSnackCreatedBy;

                    //dbEntry.Dinner = model.Dinner;
                    //dbEntry.DinnerQuantity = model.DinnerQuantity;
                    //dbEntry.DinnerDescription = model.DinnerDescription;
                    //dbEntry.DinnerVisibleToStaffOnly = model.DinnerVisibleToStaffOnly;
                    //dbEntry.DinnerCreatedDate = model.DinnerCreatedDate;
                    //dbEntry.DinnerCreatedBy = model.DinnerCreatedBy;

                    switch (model.MealType) {
                        case "Breakfast":
                            dbEntry.Breakfast = model.Breakfast;
                            dbEntry.BreakfastQuantity = model.BreakfastQuantity;
                            dbEntry.BreakfastDescription = model.BreakfastDescription;
                            dbEntry.BreakfastVisibleToStaffOnly = model.BreakfastVisibleToStaffOnly;
                            dbEntry.BreakfastCreatedDate = model.BreakfastCreatedDate;
                            dbEntry.BreakfastCreatedBy = model.BreakfastCreatedBy;
                            break;
                        case "AM Snack":
                            dbEntry.AMSnack = model.AMSnack;
                            dbEntry.AMSnackQuantity = model.AMSnackQuantity;
                            dbEntry.AMSnackDescription = model.AMSnackDescription;
                            dbEntry.AMSnackVisibleToStaffOnly = model.AMSnackVisibleToStaffOnly;
                            dbEntry.AMSnackCreatedDate = model.AMSnackCreatedDate;
                            dbEntry.AMSnackCreatedBy = model.AMSnackCreatedBy;
                            break;
                        case "Lunch":
                            dbEntry.Lunch = model.Lunch;
                            dbEntry.LunchQuantity = model.LunchQuantity;
                            dbEntry.LunchDescription = model.LunchDescription;
                            dbEntry.LunchVisibleToStaffOnly = model.LunchVisibleToStaffOnly;
                            dbEntry.LunchCreatedDate = model.LunchCreatedDate;
                            dbEntry.LunchCreatedBy = model.LunchCreatedBy;
                            break;
                        case "PM Snack":
                            dbEntry.PMSnack = model.PMSnack;
                            dbEntry.PMSnackQuantity = model.PMSnackQuantity;
                            dbEntry.PMSnackDescription = model.PMSnackDescription;
                            dbEntry.PMSnackVisibleToStaffOnly = model.PMSnackVisibleToStaffOnly;
                            dbEntry.PMSnackCreatedDate = model.PMSnackCreatedDate;
                            dbEntry.PMSnackCreatedBy = model.PMSnackCreatedBy;
                            break;
                        case "Late Snack":
                            dbEntry.LateSnack = model.LateSnack;
                            dbEntry.LateSnackQuantity = model.LateSnackQuantity;
                            dbEntry.LateSnackDescription = model.LateSnackDescription;
                            dbEntry.LateSnackVisibleToStaffOnly = model.LateSnackVisibleToStaffOnly;
                            dbEntry.LateSnackCreatedDate = model.LateSnackCreatedDate;
                            dbEntry.LateSnackCreatedBy = model.LateSnackCreatedBy;
                            break;
                        case "Dinner":
                            dbEntry.Dinner = model.Dinner;
                            dbEntry.DinnerQuantity = model.DinnerQuantity;
                            dbEntry.DinnerDescription = model.DinnerDescription;
                            dbEntry.DinnerVisibleToStaffOnly = model.DinnerVisibleToStaffOnly;
                            dbEntry.DinnerCreatedDate = model.DinnerCreatedDate;
                            dbEntry.DinnerCreatedBy = model.DinnerCreatedBy;
                            break;
                    }
                    dbEntry.UpdatedDate = DateTime.Now;// DateTime.UtcNow;
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
