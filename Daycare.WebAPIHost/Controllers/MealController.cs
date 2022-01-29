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
    public class MealController:Controller {

        IMealService mealService;
        public MealController(IMealService mealService) {
            this.mealService = mealService;
        }

        [HttpPost("getTheirChildrenMealRecordByOrganization")]
        public IActionResult getTheirChildrenMealRecordByOrganization([FromBody] Organization model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = mealService.getTheirChildrenMealRecordByOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenMealRecordByOrganization failed");
            }
        }

        [HttpPost("getTheirChildrenMealRecordByChildOrganization")]
        public IActionResult getTheirChildrenMealRecordByChildOrganization([FromBody] Child model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                var children = mealService.getTheirChildrenMealRecordByChildOrganization(model);
                return Json(children,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getTheirChildrenMealRecordByChildOrganization failed");
            }
        }

        [HttpPost("getMealOfTargetChild")]
        public IActionResult getMealOfTargetChild([FromBody] MealRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                MealRecord obj = new MealRecord();
                obj.ChildId = model.childId;
                obj.OrganizationId = model.organizationId;
                obj.TargetDate = model.targetDate;
                obj.MealType = model.mealType; //Need this for edit
                var response = mealService.getMealOfTargetChild(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "getMealOfTargetChild failed");
            }
        }

        [HttpPost("saveMealRecord")]
        public async Task<IActionResult> saveMealRecord([FromBody] MealRecordViewModel model) {
            try {
                if(model == null) { return new StatusCodeResult(500); }
                MealRecord obj = new MealRecord();
              //  obj.MealRecordId = model.mealRecordId ?? 0;
                obj.ChildId = model.childId;
                obj.Parent1Id = model.parent1Id;
                obj.Parent2Id = model.parent2Id;
                obj.OrganizationId = model.organizationId;
                obj.ChildFirstName = model.childFirstName;
                obj.ChildLastName = model.childLastName;
                obj.TargetDate = model.targetDate;

                obj.Breakfast = model.breakfast;
                obj.BreakfastQuantity = model.breakfastQuantity;
                obj.BreakfastDescription = model.breakfastDescription;
                obj.BreakfastVisibleToStaffOnly = model.breakfastVisibleToStaffOnly;
                if(model.breakfastCreatedDate!=null) {
                    obj.BreakfastCreatedDate = DateTime.Parse(model.breakfastCreatedDate);
                }
                obj.BreakfastCreatedBy = model.breakfastCreatedBy;

                obj.AMSnack = model.amSnack;
                obj.AMSnackQuantity = model.amSnackQuantity;
                obj.AMSnackDescription = model.amSnackDescription;
                obj.AMSnackVisibleToStaffOnly = model.amSnackVisibleToStaffOnly;
                if(model.amSnackCreatedDate!=null) {
                    obj.AMSnackCreatedDate = DateTime.Parse(model.amSnackCreatedDate);
                }               
                obj.AMSnackCreatedBy = model.amSnackCreatedBy;

                obj.Lunch = model.lunch;
                obj.LunchQuantity = model.lunchQuantity;
                obj.LunchDescription = model.lunchDescription;
                obj.LunchVisibleToStaffOnly = model.lunchVisibleToStaffOnly;
                if(model.lunchCreatedDate!=null) {
                    obj.LunchCreatedDate = DateTime.Parse(model.lunchCreatedDate);
                }
                obj.LunchCreatedBy = model.lunchCreatedBy;

                obj.PMSnack = model.pmSnack;
                obj.PMSnackQuantity = model.pmSnackQuantity;
                obj.PMSnackDescription = model.pmSnackDescription;
                obj.PMSnackVisibleToStaffOnly = model.pmSnackVisibleToStaffOnly;
                if(model.pmSnackCreatedDate!=null) {
                    obj.PMSnackCreatedDate = DateTime.Parse(model.pmSnackCreatedDate);
                }                
                obj.PMSnackCreatedBy = model.pmSnackCreatedBy;

                obj.LateSnack = model.lateSnack;
                obj.LateSnackQuantity = model.lateSnackQuantity;
                obj.LateSnackDescription = model.lateSnackDescription;
                obj.LateSnackVisibleToStaffOnly = model.lateSnackVisibleToStaffOnly;
                if(model.lateSnackCreatedDate!=null) {
                    obj.LateSnackCreatedDate = DateTime.Parse(model.lateSnackCreatedDate);
                }
                obj.LateSnackCreatedBy = model.lateSnackCreatedBy;

                obj.Dinner = model.dinner;
                obj.DinnerQuantity = model.dinnerQuantity;
                obj.DinnerDescription = model.dinnerDescription;
                obj.DinnerVisibleToStaffOnly = model.dinnerVisibleToStaffOnly;
                if(model.dinnerCreatedDate!=null) {
                    obj.DinnerCreatedDate = DateTime.Parse(model.dinnerCreatedDate);
                }
                obj.DinnerCreatedBy = model.dinnerCreatedBy;



                var response = mealService.saveMealRecord(obj);
                return Json(response,new JsonSerializerOptions {
                    WriteIndented = true,
                });
            } catch(Exception ex) {
                return BadRequest(ex.Message + "updateAttendanceRecord failed");
            }
        }

    }
}
