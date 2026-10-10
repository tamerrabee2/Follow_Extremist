using System;

namespace Follow_Extremist.Core
{
    public class ElementFingerprint
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public int FingerIndex { get; set; } // 0-9
        public string FingerName { get; set; } // e.g. "سبابة يمنى"
        public string TemplateData { get; set; } // Base64
        public int TemplateVersion { get; set; } = 10; // ZKFinger 10.0
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation
        public ElementInfo ElementInfo { get; set; }
    }
}
