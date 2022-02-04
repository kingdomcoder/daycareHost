using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class ChildService:IChildService {
        public IChildRepository childRepository;

        public ChildService(IChildRepository childRepository) {
            this.childRepository = childRepository;
        }

        public void CreateChild(Child model) {
            childRepository.CreateChild(model);
        }

        public void UpdateChild(Child model) {
            childRepository.UpdateChild(model);
        }

        public IEnumerable<Child> GetMyChildrenByParentId(string id) {
            return childRepository.GetMyChildrenByParentId(id);
        }

        public IEnumerable<Child> GetMyChildrenByParentUser(ApplicationUser model) {
            return childRepository.GetMyChildrenByParentUser(model);
        }

        public IEnumerable<Child> getTheirChildrenByOrganization(Organization model) {
            return childRepository.getTheirChildrenByOrganization(model);
        }

        public void PostMessage(CommentRecord model) {
            childRepository.PostMessage(model);
        }

        public Child GetChildByChildId(int id) {
            return childRepository.GetChildByChildId(id);
        }

        public IEnumerable<Activity> getActivityByChild(Child model) {
            return childRepository.getActivityByChild(model);
        }

        public void SendErrorMessageToAdmin(Child model, string message) {
            throw new NotImplementedException();
        }

        public void saveProfileFilePath(Child model) {
            childRepository.saveProfileFilePath(model);
        }
    }
}
