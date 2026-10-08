using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Follow.Data.SqlServer;

namespace Follow.Code
{
    /// <summary>
    /// خدمة النسخ الاحتياطي التلقائي لقاعدة البيانات
    /// تعمل عند تسجيل دخول مستخدم محدد مرة واحدة يومياً
    /// وتقوم بحذف النسخ القديمة بعد إنشاء النسخة الجديدة بنجاح
    /// </summary>
    public static class AutoBackupService
    {
        /// <summary>
        /// يتحقق إذا كان يجب تنفيذ النسخ الاحتياطي لهذا المستخدم اليوم
        /// </summary>
        public static bool ShouldBackup(string userName)
        {
            if (!Properties.Settings.Default.AutoBackupEnabled)
                return false;

            string targetUser = Properties.Settings.Default.AutoBackupUserName?.Trim();
            if (string.IsNullOrEmpty(targetUser))
                return false;

            // التحقق من اسم المستخدم
            if (!string.Equals(targetUser, "الكل", StringComparison.OrdinalIgnoreCase) &&
                !targetUser.Contains("الكل") &&
                !string.Equals(targetUser, "all", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(targetUser, userName?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // التحقق من تاريخ اليوم - مرة واحدة فقط في اليوم
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string lastDate = Properties.Settings.Default.LastAutoBackupDate;
            if (string.Equals(today, lastDate, StringComparison.OrdinalIgnoreCase))
            {
                return false; // تم إجراء النسخ اليوم مسبقاً
            }

            // التحقق من وجود مسار الحفظ
            string backupPath = Properties.Settings.Default.AutoBackupPath?.Trim();
            if (string.IsNullOrEmpty(backupPath))
                return false;

            return true;
        }

        /// <summary>
        /// تنفيذ النسخ الاحتياطي التلقائي وحذف النسخ القديمة
        /// </summary>
        public static async Task<bool> ExecuteAutoBackupAsync(string userName)
        {
            try
            {
                if (!ShouldBackup(userName))
                    return false;

                string backupDir = Properties.Settings.Default.AutoBackupPath.Trim();
                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                // تحديد اسم قاعدة البيانات
                string dbName;
                using (var db = new DBContext())
                {
                    dbName = db.Database.GetDbConnection().Database;
                    if (string.IsNullOrEmpty(dbName)) dbName = "FollowDataBase";
                }

                // اسم ملف النسخة الجديدة مع التاريخ والوقت
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string newBackupFileName = $"{dbName}_AutoBackup_{timestamp}.bak";
                string newBackupFullPath = Path.Combine(backupDir, newBackupFileName);

                // استعلام النسخ الاحتياطي في SQL Server
                string sqlQuery = $"BACKUP DATABASE [{dbName}] TO DISK = N'{newBackupFullPath}' WITH NOFORMAT, NOINIT, NAME = N'{dbName}_AutoBackup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

                using (var db = new DBContext())
                {
                    db.Database.SetCommandTimeout(0);
                    await db.Database.ExecuteSqlRawAsync(sqlQuery);
                }

                // التأكد من نجاح إنشاء الملف
                if (File.Exists(newBackupFullPath) && new FileInfo(newBackupFullPath).Length > 0)
                {
                    // حذف النسخ القديمة بعد التأكد من اكتمال النسخة الجديدة
                    DeleteOldBackups(backupDir, newBackupFullPath);

                    // تحديث تاريخ آخر نسخ وحفظ الإعدادات
                    Properties.Settings.Default.LastAutoBackupDate = DateTime.Today.ToString("yyyy-MM-dd");
                    Properties.Settings.Default.Save();

                    // تسجيل العملية في سجل النظام
                    try
                    {
                        var dataHelper = (Follow.Data.IDataHelper<Follow.Core.SystemRecords>)
                            ConfigurationObjectManager.GetObject("SystemRecords");
                        if (dataHelper != null)
                        {
                            await dataHelper.AddAsync(new Follow.Core.SystemRecords
                            {
                                Title = "نسخ احتياطي تلقائي",
                                USerName = userName,
                                Details = $"تم إنشاء نسخة احتياطية تلقائية ({newBackupFileName}) وحذف النسخ السابقة بنجاح.",
                                AddedDate = DateTime.Now
                            });
                        }
                    }
                    catch { }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AutoBackup Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// حذف النسخ الاحتياطية السابقة من نفس المجلد
        /// </summary>
        private static void DeleteOldBackups(string backupDir, string currentBackupFile)
        {
            try
            {
                var dir = new DirectoryInfo(backupDir);
                var oldFiles = dir.GetFiles("*AutoBackup*.bak")
                    .Where(f => !string.Equals(f.FullName, currentBackupFile, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var oldFile in oldFiles)
                {
                    try
                    {
                        oldFile.Delete();
                    }
                    catch
                    {
                        // في حال كان الملف قيد الاستخدام
                    }
                }
            }
            catch { }
        }
    }
}
