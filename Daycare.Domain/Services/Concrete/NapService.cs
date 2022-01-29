using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class NapService:INapService {
        public INapRepository napRepository;

        public NapService(INapRepository napRepository) {
            this.napRepository = napRepository;
        }

        public IEnumerable<NapRecord> getTheirChildrenNapRecordByOrganization(Organization model) {
            return napRepository.getTheirChildrenNapRecordByOrganization(model);
        }

        public IEnumerable<NapRecord> getTheirChildrenNapRecordByChildOrganization(Child model) {
            return napRepository.getTheirChildrenNapRecordByChildOrganization(model);
        }

        public NapRecord getNapOfTargetChild(NapRecord model) {
            return napRepository.getNapOfTargetChild(model);
        }

        public NapRecord saveNapRecord(NapRecord model) {
            return napRepository.saveNapRecord(model);
        }

    }
}
