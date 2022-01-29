using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daycare.WebAPIHost.ViewModels {
    public class AttendanceRecordViewModel {
        /**********************************************
         * datetime colum must be string, otherwise can not get data from client.
         * However target date can  be DateTime, beecaues it is Date only.
         * *******************************************/

        public int? attendanceRecordId { get; set; }
        public int? childId { get; set; }
        public int? organizationId { get; set; }
        public string parent1Id { get; set; }
        public string parent2Id { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
        public string imagePath { get; set; }
        public string imageFileName { get; set; }
        public DateTime? targetDate { get; set; }
        public string inTime { get; set; }              //Need to convert to DateTime
        public string inTime_StampTime { get; set; }    //Need to convert to DateTime
        public string inTime_EnteredBy { get; set; }
        public string outTime { get; set; }             //Need to convert to DateTime
        public string outTime_StampTime { get; set; }   //Need to convert to DateTime
        public string outTime_EnteredBy { get; set; }
        public bool? tardy { get; set; }
        public string tardyComment { get; set; }
        public string  tardy_StampTime { get; set; } //Need to convert to DateTime
        public string tardy_EnteredBy { get; set; }
        public bool? absent { get; set; }
        public string absentComment { get; set; }
        public string absent_StampTime { get; set; } //Need to convert to DateTime
        public string absent_EnteredBy { get; set; }
        public bool? leaveEarly { get; set; }
        public string leaveEarlyComment { get; set; }
        public string leaveEarly_StampTime { get; set; } //Need to convert to DateTime
        public string leaveEarly_EnteredBy { get; set; }
        public string cancelInTime_StampTime { get; set; } //Need to convert to DateTime
        public string cancelInTime_EnteredBy { get; set; }
        public string cancelOutTime_StampTime { get; set; } //Need to convert to DateTime
        public string cancelOutTime_EnteredBy { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }



        //public int? attendanceRecordId { get; set; }
        //public int? childId { get; set; }
        //public string parent1Id { get; set; }
        //public string parent2Id { get; set; }
        //public int? organizationId { get; set; }
        //public string childFirstName { get; set; }
        //public string childLastName { get; set; }
        ////public DateTime? inTime { get; set; }
        ////public DateTime? outTime { get; set; }
        //public string inTime { get; set; }
        //public string outTime { get; set; }
        //public bool? tardy { get; set; }
        //public bool? absent { get; set; }
        //public bool? leaveEarly { get; set; }
        //public string reason { get; set; }
        //public string memo { get; set; }
        //public DateTime? targetDate { get; set; }
        //public DateTime? recordedDate { get; set; }
        //public string imagePath { get; set; }
        //public string tardyComment { get; set; }
        //public string absentComment { get; set; }
        //public string leaveEarlyComment { get; set; }
    }
}
