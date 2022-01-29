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

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByOrganization(Organization model) {
            return childRepository.getTheirChildrenAttendanceRecordByOrganization(model);
        }

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByChildOrganization(Child model) {
            return childRepository.getTheirChildrenAttendanceRecordByChildOrganization(model);
        }

        //public IEnumerable<Activity> GetChildActivityLogByChild(Child model, DateTime? date){
        //    return childRepository.GetChildActivityLogByChild(model,date);
        //}

        public void PostMessage(CommentRecord model) {
            childRepository.PostMessage(model);
        }

        public void SendErrorMessageToAdmin(Child model, string message) {
            throw new NotImplementedException();
        }

        public Child GetChildByChildId(int id) {
            return childRepository.GetChildByChildId(id);
        }

        public IEnumerable<Activity> getActivityByChild(Child model) {
            return childRepository.getActivityByChild(model);
        }

        public AttendanceRecord getAttendanceOfTargetChild(AttendanceRecord model) {
            return childRepository.getAttendanceOfTargetChild(model);
        }

        //public AttendanceRecord updateAttendanceRecord(AttendanceRecord model) {
        //    return childRepository.updateAttendanceRecord(model);
        //}

        public AttendanceRecord updateAttendanceRecordIn(AttendanceRecord model) {
            return childRepository.updateAttendanceRecordIn(model);
        }

        public AttendanceRecord updateAttendanceRecordOut(AttendanceRecord model) {
            return childRepository.updateAttendanceRecordOut(model);
        }

        public AttendanceRecord updateAttendanceRecordTardy(AttendanceRecord model) {
            return childRepository.updateAttendanceRecordTardy(model);
        }

        public AttendanceRecord updateAttendanceRecordAbsent(AttendanceRecord model) {
            return childRepository.updateAttendanceRecordAbsent(model);
        }

        public AttendanceRecord updateAttendanceRecordLeaveEarly(AttendanceRecord model) {
            return childRepository.updateAttendanceRecordLeaveEarly(model);
        }

        public void saveProfileFilePath(Child model) {
            childRepository.saveProfileFilePath(model);
        }

        public AttendanceRecord saveTardyComment(AttendanceRecord model) {
            return childRepository.saveTardyComment(model);
        }

        public AttendanceRecord saveAbsentComment(AttendanceRecord model) {
            return childRepository.saveAbsentComment(model);
        }

        public AttendanceRecord saveLeaveEarlyComment(AttendanceRecord model) {
            return childRepository.saveLeaveEarlyComment(model);
        }


        public AttendanceRecord cancelSignIn(AttendanceRecord model) {
            return childRepository.cancelSignIn(model);
        }

        public AttendanceRecord cancelSignOut(AttendanceRecord model) {
            return childRepository.cancelSignOut(model);
        }

    }
}
