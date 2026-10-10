using System;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using Follow_Extremist.Data.SqlServer;

namespace Follow_Extremist.Code.Services
{
    public class AttendanceProcessResult
    {
        public bool Success { get; set; }
        public bool IsWanted { get; set; }
        public string Message { get; set; }
        public ElementInfo Element { get; set; }
        public AttendanceLog AttendanceLog { get; set; }
        public ElementWantedStatus WantedStatus { get; set; }
        public DateTime? NextFollowDate { get; set; }
    }

    public class AttendanceProcessor
    {
        private readonly IDataHelper<ElementInfo> dataHelperElement;
        private readonly IDataHelper<ElementFollowAdd> dataHelperFollowAdd;
        private readonly IDataHelper<AttendanceLog> dataHelperAttendanceLog;
        private readonly IDataHelper<ElementWantedStatus> dataHelperWanted;
        private readonly IDataHelper<PrintSetting> dataHelperPrintSetting;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly ThermalPrintService printService;

        public event EventHandler<AttendanceProcessResult> OnAttendanceProcessed;

        public AttendanceProcessor()
        {
            dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperFollowAdd = (IDataHelper<ElementFollowAdd>)ConfigurationObjectManager.GetObject("ElementFollowAdd");
            dataHelperAttendanceLog = (IDataHelper<AttendanceLog>)ConfigurationObjectManager.GetObject("AttendanceLog");
            dataHelperWanted = (IDataHelper<ElementWantedStatus>)ConfigurationObjectManager.GetObject("ElementWantedStatus");
            dataHelperPrintSetting = (IDataHelper<PrintSetting>)ConfigurationObjectManager.GetObject("PrintSetting");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            printService = new ThermalPrintService();
        }

        private static PrintSetting _cachedPrintSetting;
        private static DateTime _lastPrintSettingCacheTime = DateTime.MinValue;

        public static void InvalidatePrintSettingCache(PrintSetting updatedSetting = null)
        {
            _cachedPrintSetting = updatedSetting;
            _lastPrintSettingCacheTime = updatedSetting != null ? DateTime.Now : DateTime.MinValue;
        }

        private static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(SqlCon.SqlConnection))
                return SqlCon.SqlConnection;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SqServerConString))
                return Properties.Settings.Default.SqServerConString;

            return @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        private async Task<ElementInfo> FindElementFastAsync(int enrollId)
        {
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                string idStr = enrollId.ToString();
                cmd.CommandText = @"
SELECT TOP 1 e.Id, e.ElementName, e.NationalId, e.Job, e.FollowState, e.PrisonedOrnot, e.DateFollowNow, e.DateFollowNext, e.FollowDaysCount, e.DeviceEnrollId
FROM ElementInfo e
WHERE e.DeviceEnrollId = @enrollId
   OR (e.NationalId IS NOT NULL AND e.NationalId = @idStr)
   OR e.Id = @enrollId
ORDER BY 
   CASE 
      WHEN e.DeviceEnrollId = @enrollId THEN 1
      WHEN e.NationalId = @idStr THEN 2
      ELSE 3
   END";
                cmd.Parameters.AddWithValue("@enrollId", enrollId);
                cmd.Parameters.AddWithValue("@idStr", idStr);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new ElementInfo
                    {
                        Id = reader.GetInt32(0),
                        ElementName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        NationalId = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Job = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        FollowState = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        PrisonedOrnot = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        DateFollowNow = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                        DateFollowNext = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7),
                        FollowDaysCount = reader.IsDBNull(8) ? 15 : reader.GetInt32(8),
                        DeviceEnrollId = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9)
                    };
                }
            }
            catch { }

            var element = await dataHelperElement.FindAsync(enrollId);
            return element;
        }

        private async Task<ElementWantedStatus> GetWantedStatusFastAsync(int elementId)
        {
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 Id, ElementId, IsWanted, WantedReason, WantedDate, WantedBy, Notes FROM ElementWantedStatus WHERE ElementId = @elementId AND IsWanted = 1";
                cmd.Parameters.AddWithValue("@elementId", elementId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new ElementWantedStatus
                    {
                        Id = reader.GetInt32(0),
                        ElementId = reader.GetInt32(1),
                        IsWanted = reader.GetBoolean(2),
                        WantedReason = reader.IsDBNull(3) ? null : reader.GetString(3),
                        WantedDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        WantedBy = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Notes = reader.IsDBNull(6) ? null : reader.GetString(6)
                    };
                }
                return null;
            }
            catch
            {
                var allWanted = await dataHelperWanted.GetAllDataAsync();
                return allWanted?.FirstOrDefault(w => w.ElementId == elementId && w.IsWanted);
            }
        }

        private async Task<bool> IsAlreadyFollowedTodayFastAsync(int elementId, DateTime attDate)
        {
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 1 FROM ElementFollowAdd WHERE ElementInfoId = @elementId AND CAST(DateFollow AS DATE) = CAST(@attDate AS DATE)";
                cmd.Parameters.AddWithValue("@elementId", elementId);
                cmd.Parameters.AddWithValue("@attDate", attDate.Date);
                var obj = await cmd.ExecuteScalarAsync();
                return obj != null && obj != DBNull.Value;
            }
            catch
            {
                var allFollow = await dataHelperFollowAdd.GetAllDataAsync();
                return allFollow?.Any(f => f.ElementInfoId == elementId && f.DateFollow.Date == attDate.Date) ?? false;
            }
        }

        private async Task<PrintSetting> GetPrintSettingFastAsync()
        {
            if (_cachedPrintSetting != null && (DateTime.Now - _lastPrintSettingCacheTime).TotalSeconds < 30)
            {
                return _cachedPrintSetting;
            }

            try
            {
                var list = await dataHelperPrintSetting.GetAllDataAsync();
                _cachedPrintSetting = list?.FirstOrDefault() ?? new PrintSetting();
                _lastPrintSettingCacheTime = DateTime.Now;
                return _cachedPrintSetting;
            }
            catch
            {
                return _cachedPrintSetting ?? new PrintSetting();
            }
        }

        public async Task<AttendanceProcessResult> ProcessAttendanceAsync(
            int elementId,
            int? deviceId = null,
            DateTime? attendanceTime = null,
            int verifyType = 1,
            string deviceSerialNumber = null)
        {
            DateTime attTime = attendanceTime ?? DateTime.Now;
            var result = new AttendanceProcessResult();

            try
            {
                // 1. Fetch Element (Fast targeted lookup)
                var element = await FindElementFastAsync(elementId);
                if (element == null)
                {
                    result.Success = false;
                    result.Message = $"تم استقبال بصمة برقم مستخدم [ #{elementId} ] من الماكينة، ولكن هذا الرقم غير مسجل لأي عنصر في قاعدة البيانات.";
                    OnAttendanceProcessed?.Invoke(this, result);
                    return result;
                }
                result.Element = element;

                // التحقق من حالة المتابعة: العناصر التي خارج المتابعة لا تسجل حضور
                if (element.FollowState != null && element.FollowState.Trim() == "خارج المتابعة")
                {
                    result.Success = false;
                    result.AttendanceLog = null;
                    result.Message = $"العنصر {element.ElementName} مسجل كـ (خارج المتابعة)، ولا يتم تسجيل حضور له.";
                    OnAttendanceProcessed?.Invoke(this, result);
                    return result;
                }

                // التحقق من حالة الحبس: العناصر المحبوسة لا تسجل حضور
                if (element.PrisonedOrnot != null && element.PrisonedOrnot.Trim() == "محبوس")
                {
                    result.Success = false;
                    result.AttendanceLog = null;
                    result.Message = $"العنصر {element.ElementName} مسجل كـ (محبوس)، ولا يتم تسجيل حضور له.";
                    OnAttendanceProcessed?.Invoke(this, result);
                    return result;
                }

                // 2. Check Wanted Status (Fast 1ms targeted query)
                var wantedRecord = await GetWantedStatusFastAsync(element.Id);
                result.WantedStatus = wantedRecord;
                result.IsWanted = (wantedRecord != null && wantedRecord.IsWanted);

                // Load print settings (Cached in memory)
                var printSetting = await GetPrintSettingFastAsync();

                if (result.IsWanted)
                {
                    // === WANTED ELEMENT FLOW ===
                    // تعديل ميعاد متابعته الجديدة وتسجيل حضوره في قاعدة البيانات
                    int daysInterval = element.FollowDaysCount > 0 ? element.FollowDaysCount : 15;
                    DateTime nextFollowNow = attTime.Date.AddDays(daysInterval);
                    DateTime nextFollowNext = attTime.Date.AddDays(daysInterval * 2);

                    element.DateFollowNow = nextFollowNow;
                    element.DateFollowNext = nextFollowNext;
                    await dataHelperElement.EditAsync(element);

                    // إضافة في سجل متابعات العناصر اليومية ElementFollowAdd
                    var followAddRecord = new ElementFollowAdd
                    {
                        ElementName = element.ElementName,
                        DateFollow = attTime.Date,
                        ElementInfoId = element.Id
                    };
                    await dataHelperFollowAdd.AddAsync(followAddRecord);

                    var attLog = new AttendanceLog
                    {
                        ElementId = element.Id,
                        DeviceEnrollId = element.DeviceEnrollId ?? (elementId > 0 ? elementId : (int?)null),
                        DeviceId = deviceId,
                        DeviceSerialNumber = deviceSerialNumber,
                        AttendanceDateTime = attTime,
                        VerifyType = verifyType,
                        IsWantedAtTime = true,
                        NextFollowDateAssigned = nextFollowNow, // يتم حفظ ميعاد المتابعة القادم
                        Status = "مطلوب",
                        Notes = $"مطلوب: {wantedRecord?.WantedReason} - {wantedRecord?.WantedBy}"
                    };

                    await dataHelperAttendanceLog.AddAsync(attLog);
                    result.AttendanceLog = attLog;
                    result.NextFollowDate = nextFollowNow;
                    result.Success = true;
                    result.Message = $"تنبيه أمني: العنصر {element.ElementName} مـطـلـوب!";

                    // Audit System Record
                    if (dataHelperSystemRecords != null)
                    {
                        await dataHelperSystemRecords.AddAsync(new SystemRecords
                        {
                            Title = "تنبيه: حضور عنصر مطلوب",
                            USerName = Properties.Settings.Default.UserName ?? "نظام البصمة",
                            Details = $"حضر العنصر المطلوب: {element.ElementName} (رقم قومي: {element.NationalId}) - سبب: {wantedRecord?.WantedReason}",
                            AddedDate = DateTime.Now
                        });
                    }

                    // Auto Print if enabled (shows WANTED and NO next date)
                    bool canPrint = printSetting.AutoPrintOnAttendance && printSetting.OutputMode != "ScreenOnly";
                    if (canPrint)
                    {
                        printService.PrintAttendanceSlip(attLog, element, wantedRecord, printSetting, false);
                        attLog.IsPrinted = true;
                        attLog.PrintedDate = DateTime.Now;
                        await dataHelperAttendanceLog.EditAsync(attLog);
                    }
                }
                else
                {
                    // === NORMAL ELEMENT FLOW ===
                    // 1. التحقق مما إذا كان موعد متابعة العنصر قد حان
                    // الشرط الصارم: لا يتم تسجيل حضور بصمة في جدول البصمات إلا عندما يحين موعد متابعة العنصر
                    if (element.DateFollowNow.Date > attTime.Date)
                    {
                        result.Success = false;
                        result.AttendanceLog = null;
                        result.NextFollowDate = element.DateFollowNow;
                        result.Message = $"لم يحن موعد متابعة العنصر {element.ElementName} بعد. موعد المتابعة المحدد هو: {element.DateFollowNow:yyyy/MM/dd}";
                        OnAttendanceProcessed?.Invoke(this, result);
                        return result;
                    }

                    // 2. التحقق مما إذا كان قد تم تسجيل المتابعة اليوم مسبقاً في جدول المتابعات (استعلام فوري سريع 1ms)
                    var alreadyToday = await IsAlreadyFollowedTodayFastAsync(element.Id, attTime);
                    if (alreadyToday)
                    {
                        result.Success = false;
                        result.AttendanceLog = null;
                        result.NextFollowDate = element.DateFollowNow;
                        result.Message = $"العنصر {element.ElementName} تم تسجيل متابعته اليوم مسبقاً. موعد المتابعة القادم: {element.DateFollowNow:yyyy/MM/dd}";
                        OnAttendanceProcessed?.Invoke(this, result);
                        return result;
                    }

                    // 3. حان موعد المتابعة -> يتم احتساب المتابعة القادمة وتحديث جدول العناصر اتوماتيكياً
                    int daysInterval = element.FollowDaysCount > 0 ? element.FollowDaysCount : 15;
                    DateTime nextFollowNow = attTime.Date.AddDays(daysInterval);
                    DateTime nextFollowNext = attTime.Date.AddDays(daysInterval * 2);

                    // تحديث ميعاد المتابعة القادم في جدول العناصر اتوماتيك
                    element.DateFollowNow = nextFollowNow;
                    element.DateFollowNext = nextFollowNext;
                    await dataHelperElement.EditAsync(element);

                    // إضافة في سجل متابعات العناصر اليومية ElementFollowAdd
                    var followAddRecord = new ElementFollowAdd
                    {
                        ElementName = element.ElementName,
                        DateFollow = attTime.Date,
                        ElementInfoId = element.Id
                    };
                    await dataHelperFollowAdd.AddAsync(followAddRecord);

                    result.NextFollowDate = nextFollowNow;

                    // إضافة في سجلات النظام
                    if (dataHelperSystemRecords != null)
                    {
                        await dataHelperSystemRecords.AddAsync(new SystemRecords
                        {
                            Title = "تسجيل حضور ومتابعة بالبصمة",
                            USerName = Properties.Settings.Default.UserName ?? "نظام البصمة",
                            Details = $"تم تسجيل حضور ومتابعة بالبصمة للعنصر {element.ElementName} والمتابعة القادمة: {nextFollowNow:yyyy/MM/dd}",
                            AddedDate = DateTime.Now
                        });
                    }

                    // تسجيل حضور البصمة في جدول البصمات (AttendanceLog) فقط عند حلول موعد المتابعة
                    var attLog = new AttendanceLog
                    {
                        ElementId = element.Id,
                        DeviceEnrollId = element.DeviceEnrollId ?? (elementId > 0 ? elementId : (int?)null),
                        DeviceId = deviceId,
                        DeviceSerialNumber = deviceSerialNumber,
                        AttendanceDateTime = attTime,
                        VerifyType = verifyType,
                        IsWantedAtTime = false,
                        NextFollowDateAssigned = nextFollowNow,
                        Status = "تم بنجاح"
                    };

                    await dataHelperAttendanceLog.AddAsync(attLog);
                    result.AttendanceLog = attLog;
                    result.Success = true;
                    result.Message = $"تم تسجيل حضور ومتابعة العنصر {element.ElementName} بنجاح. موعد المتابعة القادم: {nextFollowNow:yyyy/MM/dd}";

                    // طباعة الإيصال إن كانت مفعلة
                    bool canPrintNormal = printSetting.AutoPrintOnAttendance && printSetting.OutputMode != "ScreenOnly";
                    if (canPrintNormal)
                    {
                        printService.PrintAttendanceSlip(attLog, element, null, printSetting, false);
                        attLog.IsPrinted = true;
                        attLog.PrintedDate = DateTime.Now;
                        await dataHelperAttendanceLog.EditAsync(attLog);
                    }
                }

                OnAttendanceProcessed?.Invoke(this, result);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"خطأ أثناء معالجة الحضور: {ex.Message}";
                OnAttendanceProcessed?.Invoke(this, result);
                return result;
            }
        }
    }
}
