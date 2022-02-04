using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Abstract {
    public interface IAttendanceService {

        IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByOrganization(Organization model);

        IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByChildOrganization(Child model);

        void PostMessage(CommentRecord model);

        Child GetChildByChildId(int id);

        IEnumerable<Activity> getActivityByChild(Child model);

        AttendanceRecord getAttendanceOfTargetChild(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordIn(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordOut(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordTardy(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordAbsent(AttendanceRecord model);

        AttendanceRecord updateAttendanceRecordLeaveEarly(AttendanceRecord model);

        AttendanceRecord saveTardyComment(AttendanceRecord model);

        AttendanceRecord saveAbsentComment(AttendanceRecord model);

        AttendanceRecord saveLeaveEarlyComment(AttendanceRecord model);

        AttendanceRecord cancelSignIn(AttendanceRecord model);

        AttendanceRecord cancelSignOut(AttendanceRecord model);
    }
}
