using System;

namespace Follow_Extremist.Core
{
    public class TodayAttendanceView
    {
        public int AttendanceLogId { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public string NationalId { get; set; }
        public DateTime AttendanceDateTime { get; set; }
        public string DeviceName { get; set; }
        public bool IsWanted { get; set; }
        public DateTime? NextFollowDate { get; set; }
        public bool IsPrinted { get; set; }
        public string Status { get; set; }
    }
}
