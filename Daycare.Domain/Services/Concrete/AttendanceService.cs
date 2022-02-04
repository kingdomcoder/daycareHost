using Daycare.Domain.Entities;
using Daycare.Domain.Entities.Daycare;
using Daycare.Domain.Repositories.Abstract;
using Daycare.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daycare.Domain.Services.Concrete {
    public class AttendanceService:IAttendanceService {
        public IAttendanceRepository attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository) {
            this.attendanceRepository = attendanceRepository;
        }

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByOrganization(Organization model) {
            return attendanceRepository.getTheirChildrenAttendanceRecordByOrganization(model);
        }

        public IEnumerable<AttendanceRecord> getTheirChildrenAttendanceRecordByChildOrganization(Child model) {
            return attendanceRepository.getTheirChildrenAttendanceRecordByChildOrganization(model);
        }

        public void PostMessage(CommentRecord model) {
            attendanceRepository.PostMessage(model);
        }

        public Child GetChildByChildId(int id) {
            return attendanceRepository.GetChildByChildId(id);
        }

        public IEnumerable<Activity> getActivityByChild(Child model) {
            return attendanceRepository.getActivityByChild(model);
        }

        public AttendanceRecord getAttendanceOfTargetChild(AttendanceRecord model) {
            return attendanceRepository.getAttendanceOfTargetChild(model);
        }

        public AttendanceRecord updateAttendanceRecordIn(AttendanceRecord model) {
            return attendanceRepository.updateAttendanceRecordIn(model);
        }

        public AttendanceRecord updateAttendanceRecordOut(AttendanceRecord model) {
            return attendanceRepository.updateAttendanceRecordOut(model);
        }

        public AttendanceRecord updateAttendanceRecordTardy(AttendanceRecord model) {
            return attendanceRepository.updateAttendanceRecordTardy(model);
        }

        public AttendanceRecord updateAttendanceRecordAbsent(AttendanceRecord model) {
            return attendanceRepository.updateAttendanceRecordAbsent(model);
        }

        public AttendanceRecord updateAttendanceRecordLeaveEarly(AttendanceRecord model) {
            return attendanceRepository.updateAttendanceRecordLeaveEarly(model);
        }

        public AttendanceRecord saveTardyComment(AttendanceRecord model) {
            return attendanceRepository.saveTardyComment(model);
        }

        public AttendanceRecord saveAbsentComment(AttendanceRecord model) {
            return attendanceRepository.saveAbsentComment(model);
        }

        public AttendanceRecord saveLeaveEarlyComment(AttendanceRecord model) {
            return attendanceRepository.saveLeaveEarlyComment(model);
        }

        public AttendanceRecord cancelSignIn(AttendanceRecord model) {
            return attendanceRepository.cancelSignIn(model);
        }

        public AttendanceRecord cancelSignOut(AttendanceRecord model) {
            return attendanceRepository.cancelSignOut(model);
        }
    }
}
