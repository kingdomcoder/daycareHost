using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Daycare.Domain.Repositories.Concrete {
    public class EFRegistrationRepository:IRegistrationRepository {
        private MyDbContext context;
        private MyUserDbContext userContext;

        public EFRegistrationRepository(MyDbContext context, MyUserDbContext userContext) {
            this.context = context;
            this.userContext = userContext;
        }

        public Organization createOrganization(Organization model){
            try {
                // 1) Add new organization
                var result = (from table in context.Organization
                                where
                                table.OrganizationName == model.OrganizationName &&
                                table.ContactEmail == model.ContactEmail 
                                select table
                                ).FirstOrDefault();

                if (result == null) {
                    if (model.RegisteredDate != null) {
                        /********************************************************************************
                         * This adjustment is because wherever user is, default time for insert is 8:00am.
                         * Need to add 4hours so that UTC becomes 12:00pm. 12:00pm is best because wherever
                         * user is, it returns same date.
                         * *******************************************************************************/
                        model.RegisteredDate = model.RegisteredDate.Value.AddHours(4);
                    }
                    context.Organization.Add(model);
                    context.SaveChanges();
                }
                // 2) Update owner's profile with the new organization name and Id.
                var newOrganization = context.Organization.Where(x => x.OrganizationName == model.OrganizationName &&
                x.ContactEmail == model.ContactEmail).FirstOrDefault();
                return newOrganization;
            } catch (Exception ex) {
                throw new Exception("Error at UpdateMasterConference: " + ex.Message);
            }
        }

        public Organization getOrganizationById(int id) {
            try {
                var result = (from table in context.Organization
                              where
                              table.OrganizationId == id
                              select table
                                ).FirstOrDefault();
                if (result != null) {
                    return result;
                } else {
                    throw new Exception("No organization found");
                }
            } catch (Exception ex) {
                throw new Exception("Error at getOrganizationById: " + ex.Message);
            }
        }
    }

    
}
