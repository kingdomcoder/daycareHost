using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IMealRepository {
        IEnumerable<MealRecord> getTheirChildrenMealRecordByOrganization(Organization model);

        IEnumerable<MealRecord> getTheirChildrenMealRecordByChildOrganization(Child model);

        MealRecord getMealOfTargetChild(MealRecord model);

        MealRecord saveMealRecord(MealRecord model);
    }
}
