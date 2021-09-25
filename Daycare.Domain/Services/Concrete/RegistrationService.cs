using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class RegistrationService:IRegistrationService {
        IRegistrationRepository registationRepository;
        public RegistrationService() { }
        public RegistrationService(IRegistrationRepository registrationRepository) {
            this.registationRepository = registrationRepository;
        }

        public Organization createOrganization(Organization model) {
            return registationRepository.createOrganization(model);
        }

        public Organization  getOrganizationById(int id) {
            return registationRepository.getOrganizationById(id);
        }


    }
}
