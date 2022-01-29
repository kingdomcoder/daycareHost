using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface INapService {
        IEnumerable<NapRecord> getTheirChildrenNapRecordByOrganization(Organization model);

        IEnumerable<NapRecord> getTheirChildrenNapRecordByChildOrganization(Child model);

        NapRecord getNapOfTargetChild(NapRecord model);

        NapRecord saveNapRecord(NapRecord model);

    }
}
