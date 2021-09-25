using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IRegistrationService {

        Organization createOrganization(Organization model);

        Organization getOrganizationById(int id);

    }
}
