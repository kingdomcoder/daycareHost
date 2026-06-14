using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {

        public interface IUserRepository {

            ApplicationUser GetUserProfileById(string id);

            IEnumerable<ApplicationUser> User { get; }

            ApplicationUser updateUser(ApplicationUser user, string password);

            void updateUserConference(ApplicationUser user);

            void UpdateUser(ApplicationUser user);

            IEnumerable<ApplicationUser> GetUsersBySearchKey(ApplicationUser model);

            void updateLastAccessedDate(ApplicationUser user);

            void DeleteUserById(string Id);

            ApplicationUser GetUserByTelNo(string telNo);

    }

}
