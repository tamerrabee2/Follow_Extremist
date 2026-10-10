using System;

namespace Follow_Extremist.Core
{
    public class ElementWantedStatus
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public bool IsWanted { get; set; } = false;
        public string WantedReason { get; set; }
        public DateTime? WantedDate { get; set; }
        public string WantedBy { get; set; } // الجهة الطالبة
        public string Notes { get; set; }

        // Navigation (1:1 with ElementInfo)
        public ElementInfo ElementInfo { get; set; }

        public override string ToString()
        {
            return IsWanted ? "مطلوب" : "غير مطلوب";
        }
    }
}
