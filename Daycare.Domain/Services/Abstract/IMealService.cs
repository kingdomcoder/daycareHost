using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IMealService {

         IEnumerable<MealRecord> getTheirChildrenMealRecordByOrganization(Organization model);

        IEnumerable<MealRecord> getTheirChildrenMealRecordByChildOrganization(Child model);

        MealRecord getMealOfTargetChild(MealRecord model);

        MealRecord saveMealRecord(MealRecord model);
    }
}