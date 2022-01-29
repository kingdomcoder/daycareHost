using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IPottyService {
        IEnumerable<PottyRecord> getTheirChildrenPottyRecordByOrganization(Organization model);

        IEnumerable<PottyRecord> getTheirChildrenPottyRecordByChildOrganization(Child model);

        PottyRecord getPottyOfTargetChild(PottyRecord model);

        PottyRecord savePottyRecord(PottyRecord model);

    }
}
