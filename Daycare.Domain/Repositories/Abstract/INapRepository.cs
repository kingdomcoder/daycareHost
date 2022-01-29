using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface INapRepository {
        IEnumerable<NapRecord> getTheirChildrenNapRecordByOrganization(Organization model);

        IEnumerable<NapRecord> getTheirChildrenNapRecordByChildOrganization(Child model);

        NapRecord getNapOfTargetChild(NapRecord model);

        NapRecord saveNapRecord(NapRecord model);
    }
}
