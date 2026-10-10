using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Data.SqlServer
{
    public class AttendanceLogEntity : IDataHelper<AttendanceLog>
    {
        private DBContext db;
        private AttendanceLog table;

        public AttendanceLogEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(AttendanceLog table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.AttendanceLog.Add(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync(AttendanceLog table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    await db.AttendanceLog.AddAsync(table);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    table = Find(Id);
                    if (table != null)
                    {
                        db.AttendanceLog.Remove(table);
                        db.SaveChanges();
                        return 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    table = await FindAsync(Id);
                    if (table != null)
                    {
                        db.AttendanceLog.Remove(table);
                        await db.SaveChangesAsync();
                        return 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int Edit(AttendanceLog table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.AttendanceLog.Update(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(AttendanceLog table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.AttendanceLog.Update(table));
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public AttendanceLog Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.AttendanceLog
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .FirstOrDefault(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<AttendanceLog> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.AttendanceLog
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .FirstOrDefaultAsync(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<AttendanceLog> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.AttendanceLog
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AttendanceLog>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.AttendanceLog
                        .AsNoTracking()
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<AttendanceLog> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.AttendanceLog
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .Where(x =>
                            x.Id.ToString() == SearchItem ||
                            x.ElementId.ToString() == SearchItem ||
                            x.Status.Contains(SearchItem) ||
                            (x.ElementInfo != null && (x.ElementInfo.ElementName.Contains(SearchItem) || x.ElementInfo.NationalId.Contains(SearchItem)))
                        )
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AttendanceLog>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.AttendanceLog
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .Where(x =>
                            x.Id.ToString() == SearchItem ||
                            x.ElementId.ToString() == SearchItem ||
                            x.Status.Contains(SearchItem) ||
                            (x.ElementInfo != null && (x.ElementInfo.ElementName.Contains(SearchItem) || x.ElementInfo.NationalId.Contains(SearchItem)))
                        )
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AttendanceLog>> GetFilteredDataAsync()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                if (await db.Database.CanConnectAsync())
                {
                    return await db.AttendanceLog
                        .AsNoTracking()
                        .Include(x => x.ElementInfo)
                        .Include(x => x.FingerprintDevice)
                        .Where(x => x.AttendanceDateTime >= today && x.AttendanceDateTime < tomorrow)
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToListAsync();
                }
                return await GetTodayLogsDirectSqlAsync();
            }
            catch
            {
                return await GetTodayLogsDirectSqlAsync();
            }
        }

        private async Task<List<AttendanceLog>> GetTodayLogsDirectSqlAsync()
        {
            var list = new List<AttendanceLog>();
            try
            {
                string conStr = !string.IsNullOrEmpty(SqlCon.SqlConnection)
                    ? SqlCon.SqlConnection
                    : @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(conStr);
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT a.Id, a.ElementId, a.DeviceId, a.AttendanceDateTime, a.VerifyType, a.IsWantedAtTime, 
       a.NextFollowDateAssigned, a.IsPrinted, a.PrintedDate, a.PrintType, a.Status, a.Notes,
       e.ElementName, e.NationalId, e.Job, d.DeviceName
FROM AttendanceLog a
LEFT JOIN ElementInfo e ON a.ElementId = e.Id
LEFT JOIN FingerprintDevice d ON a.DeviceId = d.Id
WHERE CAST(a.AttendanceDateTime AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY a.AttendanceDateTime DESC;";
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var log = new AttendanceLog
                    {
                        Id = reader.GetInt32(0),
                        ElementId = reader.GetInt32(1),
                        DeviceId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        AttendanceDateTime = reader.GetDateTime(3),
                        VerifyType = reader.GetInt32(4),
                        IsWantedAtTime = reader.GetBoolean(5),
                        NextFollowDateAssigned = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                        IsPrinted = reader.GetBoolean(7),
                        PrintedDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        PrintType = reader.IsDBNull(9) ? "" : reader.GetString(9),
                        Status = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        Notes = reader.IsDBNull(11) ? "" : reader.GetString(11),
                        ElementInfo = new ElementInfo
                        {
                            Id = reader.GetInt32(1),
                            ElementName = reader.IsDBNull(12) ? "" : reader.GetString(12),
                            NationalId = reader.IsDBNull(13) ? "" : reader.GetString(13),
                            Job = reader.IsDBNull(14) ? "" : reader.GetString(14)
                        }
                    };
                    if (!reader.IsDBNull(2))
                    {
                        log.FingerprintDevice = new FingerprintDevice
                        {
                            Id = reader.GetInt32(2),
                            DeviceName = reader.IsDBNull(15) ? "" : reader.GetString(15)
                        };
                    }
                    list.Add(log);
                }
            }
            catch { }
            return list;
        }
        #endregion
    }
}
