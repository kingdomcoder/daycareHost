using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IRegistrationRepository {

        Organization createOrganization(Organization model);

        Organization getOrganizationById(int id);
    }
}
