namespace Follow_Extremist.Core
{
    public class PrintSetting
    {
        public int Id { get; set; }
        public string PrinterType { get; set; } = "Thermal"; // "Thermal" or "LaserA4"
        public string PrinterName { get; set; }
        public int PaperWidthMm { get; set; } = 80; // 80mm or 58mm
        public bool AutoPrintOnAttendance { get; set; } = true;
        public int PrintCopies { get; set; } = 1;
        public string HeaderText { get; set; } = "حضور متابعة";
        public string FooterText { get; set; } = "يرجى الاحتفاظ بهذا الإشعار والالتزام بموعد المتابعة";
        public bool ShowBarcode { get; set; } = true;
        public bool ShowNationalId { get; set; } = true;
        public bool ShowNextFollowDate { get; set; } = true;
        public string OutputMode { get; set; } = "Both"; // "Both", "ScreenOnly", "PrintOnly"
        public int ScreenDurationSeconds { get; set; } = 12;
    }
}
