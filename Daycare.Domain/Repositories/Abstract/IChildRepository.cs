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

        IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByOrganization(Organization model);

        IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByChildOrganization(Child model);

        // IEnumerable<Activity> GetChildActivityLogByChild(Child model, DateTime? date);

        void PostMessage(CommentRecord model);

        void SendErrorMessageToAdmin(Child model, string message);

        Child GetChildByChildId(int id);

        IEnumerable<Activity> getActivityByChild(Child model);

        AttendanceRecord getAttendanceOfTargetChild(AttendanceRecord model);

       // AttendanceRecord updateAttendanceRecord(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordIn(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordOut(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordTardy(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordAbsent(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordLeaveEarly(AttendanceRecord model);

        void saveProfileFilePath(Child model);

        AttendanceRecord saveTardyComment(AttendanceRecord model);

        AttendanceRecord saveAbsentComment(AttendanceRecord model);

        AttendanceRecord saveLeaveEarlyComment(AttendanceRecord model);

        AttendanceRecord cancelSignIn(AttendanceRecord model);

        AttendanceRecord cancelSignOut(AttendanceRecord model);

    }
}
