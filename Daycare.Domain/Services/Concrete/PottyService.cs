using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class PottyService:IPottyService {
        public IPottyRepository pottyRepository;

        public PottyService(IPottyRepository pottyRepository) {
            this.pottyRepository = pottyRepository;
        }

        public IEnumerable<PottyRecord> getTheirChildrenPottyRecordByOrganization(Organization model) {
            return pottyRepository.getTheirChildrenPottyRecordByOrganization(model);
        }

        public IEnumerable<PottyRecord> getTheirChildrenPottyRecordByChildOrganization(Child model) {
            return pottyRepository.getTheirChildrenPottyRecordByChildOrganization(model);
        }

        public PottyRecord getPottyOfTargetChild(PottyRecord model) {
            return pottyRepository.getPottyOfTargetChild(model);
        }

        public PottyRecord savePottyRecord(PottyRecord model) {
            return pottyRepository.savePottyRecord(model);
        }

    }
}
