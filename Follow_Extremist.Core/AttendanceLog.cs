using System;

namespace Follow_Extremist.Core
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public int? DeviceEnrollId { get; set; } // كود البصمة القادم من الماكينة
        public int? DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; } // سيريال جهاز البصمة الفعلي
        public DateTime AttendanceDateTime { get; set; }
        public int VerifyType { get; set; } = 1; // 1: بصمة, 2: بطاقة, 3: كلمة سر, 4: وجه
        public bool IsWantedAtTime { get; set; } = false;
        public DateTime? NextFollowDateAssigned { get; set; }
        public bool IsPrinted { get; set; } = false;
        public DateTime? PrintedDate { get; set; }
        public string PrintType { get; set; } // "حراري" أو "A4"
        public string Status { get; set; } = "تم بنجاح"; // تم بنجاح / مطلوب / خطأ
        public string Notes { get; set; }

        // Navigation
        public ElementInfo ElementInfo { get; set; }
        public FingerprintDevice FingerprintDevice { get; set; }
    }
}
