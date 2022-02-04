using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Repositories.Abstract {
    public interface IChildRepository {

        void CreateChild(Child model);

        void UpdateChild(Child model);

        IEnumerable<Child> GetMyChildrenByParentId(string id);

        IEnumerable<Child> GetMyChildrenByParentUser(ApplicationUser model);

        IEnumerable<Child> getTheirChildrenByOrganization(Organization model);

        void PostMessage(CommentRecord model);

        Child GetChildByChildId(int id);

        IEnumerable<Activity> getActivityByChild(Child model);

        void SendErrorMessageToAdmin(Child model, string message);

        void saveProfileFilePath(Child model);
    }
}
