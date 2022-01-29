using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IPottyRepository {
        IEnumerable<PottyRecord> getTheirChildrenPottyRecordByOrganization(Organization model);

        IEnumerable<PottyRecord> getTheirChildrenPottyRecordByChildOrganization(Child model);

        PottyRecord getPottyOfTargetChild(PottyRecord model);

        PottyRecord savePottyRecord(PottyRecord model);
    }
}
