using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class MealService:IMealService {
        public IMealRepository mealRepository;

        public MealService(IMealRepository mealRepository) {
            this.mealRepository = mealRepository;
        }

        public IEnumerable<MealRecord> getTheirChildrenMealRecordByOrganization(Organization model) {
            return mealRepository.getTheirChildrenMealRecordByOrganization(model);
        }

        public IEnumerable<MealRecord> getTheirChildrenMealRecordByChildOrganization(Child model) {
            return mealRepository.getTheirChildrenMealRecordByChildOrganization(model);
        }

        public MealRecord getMealOfTargetChild(MealRecord model) {
            return mealRepository.getMealOfTargetChild(model);
        }

        public MealRecord saveMealRecord(MealRecord model) {
            return mealRepository.saveMealRecord(model);
        }

    }
}
