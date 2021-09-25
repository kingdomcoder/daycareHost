using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IChildService {
        public void CreateChild(Child model);

        void UpdateChild(Child model);

        IEnumerable<Child> GetMyChildrenByParentId(string id);

        IEnumerable<Child> GetMyChildrenByParentUser(ApplicationUser model);

        IEnumerable<Child> getTheirChildrenByOrganization(Organization model);

        //  IEnumerable<Activity> GetChildActivityLogByChild(Child model,DateTime? date);

        void PostMessage(CommentRecord model);

        void SendErrorMessageToAdmin(Child model, string message);

        Child GetChildByChildId(int id);

        IEnumerable<Activity> getActivityByChild(Child model);

    }
}
