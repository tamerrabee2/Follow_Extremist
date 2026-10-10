using System;
using System.Collections.Generic;

namespace Follow_Extremist.Core
{
    public class FingerprintDevice
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; } = 4370;
        public int MachineNumber { get; set; } = 1;
        public string SerialNumber { get; set; } // السيريال نمبر الخاص بعتاد الماكينة
        public string CommPassword { get; set; }
        public string Location { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime? LastSyncTime { get; set; }
        public string Status { get; set; } = "غير متصل";
        public string Notes { get; set; }

        public List<AttendanceLog> AttendanceLogs { get; set; } = new();
    }
}
