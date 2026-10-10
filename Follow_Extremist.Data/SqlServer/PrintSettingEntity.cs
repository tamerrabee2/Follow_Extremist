using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Data.SqlServer
{
    public class PrintSettingEntity : IDataHelper<PrintSetting>
    {
        private DBContext db;
        private PrintSetting table;

        public PrintSettingEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(PrintSetting table)
        {
            try
            {
                db = new DBContext();
                db.PrintSetting.Add(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return SaveViaDirectSql(table);
            }
        }

        public async Task<int> AddAsync(PrintSetting table)
        {
            try
            {
                db = new DBContext();
                await db.PrintSetting.AddAsync(table);
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return await SaveViaDirectSqlAsync(table);
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
                        db.PrintSetting.Remove(table);
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
                        db.PrintSetting.Remove(table);
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

        public int Edit(PrintSetting table)
        {
            try
            {
                db = new DBContext();
                db.PrintSetting.Update(table);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return SaveViaDirectSql(table);
            }
        }

        public async Task<int> EditAsync(PrintSetting table)
        {
            try
            {
                db = new DBContext();
                await Task.Run(() => db.PrintSetting.Update(table));
                await db.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return await SaveViaDirectSqlAsync(table);
            }
        }

        public PrintSetting Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    var found = db.PrintSetting.FirstOrDefault(x => x.Id == Id);
                    if (found != null) return found;
                }
                return GetAllDataDirectSql().FirstOrDefault(x => x.Id == Id);
            }
            catch
            {
                return GetAllDataDirectSql().FirstOrDefault(x => x.Id == Id);
            }
        }

        public async Task<PrintSetting> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    var found = await Task.Run(() => db.PrintSetting.FirstOrDefault(x => x.Id == Id));
                    if (found != null) return found;
                }
                var directList = await GetAllDataDirectSqlAsync();
                return directList.FirstOrDefault(x => x.Id == Id);
            }
            catch
            {
                var directList = await GetAllDataDirectSqlAsync();
                return directList.FirstOrDefault(x => x.Id == Id);
            }
        }

        public List<PrintSetting> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    var list = db.PrintSetting.ToList();
                    if (list != null && list.Count > 0) return list;
                }
                return GetAllDataDirectSql();
            }
            catch
            {
                return GetAllDataDirectSql();
            }
        }

        public async Task<List<PrintSetting>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    var list = await Task.Run(() => db.PrintSetting.ToList());
                    if (list != null && list.Count > 0) return list;
                }
                return await GetAllDataDirectSqlAsync();
            }
            catch
            {
                return await GetAllDataDirectSqlAsync();
            }
        }

        public List<PrintSetting> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.PrintSetting.Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.PrinterName.Contains(SearchItem) ||
                        x.PrinterType.Contains(SearchItem)
                    ).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PrintSetting>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.PrintSetting.Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.PrinterName.Contains(SearchItem) ||
                        x.PrinterType.Contains(SearchItem)
                    ).ToList());
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<PrintSetting>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }

        private static string GetConnectionString()
        {
            return !string.IsNullOrEmpty(SqlCon.SqlConnection)
                ? SqlCon.SqlConnection
                : @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        private static bool _schemaEnsured = false;
        private static readonly object _schemaLock = new object();

        public static async Task EnsureTableAndColumnsExistAsync()
        {
            if (_schemaEnsured) return;
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'PrintSetting')
BEGIN
    CREATE TABLE [dbo].[PrintSetting] (
        [Id]                    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [PrinterType]           NVARCHAR(MAX)     NULL,
        [PrinterName]           NVARCHAR(MAX)     NULL,
        [PaperWidthMm]          INT               NOT NULL DEFAULT 80,
        [AutoPrintOnAttendance] BIT               NOT NULL DEFAULT 1,
        [PrintCopies]           INT               NOT NULL DEFAULT 1,
        [HeaderText]            NVARCHAR(MAX)     NULL,
        [FooterText]            NVARCHAR(MAX)     NULL,
        [ShowBarcode]           BIT               NOT NULL DEFAULT 1,
        [ShowNationalId]        BIT               NOT NULL DEFAULT 1,
        [ShowNextFollowDate]    BIT               NOT NULL DEFAULT 1,
        [OutputMode]            NVARCHAR(MAX)     NULL DEFAULT N'Both',
        [ScreenDurationSeconds] INT               NOT NULL DEFAULT 12
    );
END
ELSE
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'OutputMode')
        ALTER TABLE [dbo].[PrintSetting] ADD [OutputMode] NVARCHAR(MAX) NULL DEFAULT N'Both';

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'ScreenDurationSeconds')
        ALTER TABLE [dbo].[PrintSetting] ADD [ScreenDurationSeconds] INT NOT NULL DEFAULT 12;
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[PrintSetting])
BEGIN
    INSERT INTO [dbo].[PrintSetting] (
        [PrinterType], [PrinterName], [PaperWidthMm], [AutoPrintOnAttendance], 
        [PrintCopies], [HeaderText], [FooterText], [ShowBarcode], 
        [ShowNationalId], [ShowNextFollowDate], [OutputMode], [ScreenDurationSeconds]
    ) VALUES (
        N'Thermal', N'', 80, 1, 1, 
        N'حضور متابعة', N'يرجى الالتزام بموعد المتابعة القادم', 1, 1, 1, 
        N'Both', 12
    );
END";
                await cmd.ExecuteNonQueryAsync();
                _schemaEnsured = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EnsureTableAndColumnsExistAsync error: {ex.Message}");
            }
        }

        public static void EnsureTableAndColumnsExist()
        {
            if (_schemaEnsured) return;
            lock (_schemaLock)
            {
                if (_schemaEnsured) return;
                try
                {
                    using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'PrintSetting')
BEGIN
    CREATE TABLE [dbo].[PrintSetting] (
        [Id]                    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [PrinterType]           NVARCHAR(MAX)     NULL,
        [PrinterName]           NVARCHAR(MAX)     NULL,
        [PaperWidthMm]          INT               NOT NULL DEFAULT 80,
        [AutoPrintOnAttendance] BIT               NOT NULL DEFAULT 1,
        [PrintCopies]           INT               NOT NULL DEFAULT 1,
        [HeaderText]            NVARCHAR(MAX)     NULL,
        [FooterText]            NVARCHAR(MAX)     NULL,
        [ShowBarcode]           BIT               NOT NULL DEFAULT 1,
        [ShowNationalId]        BIT               NOT NULL DEFAULT 1,
        [ShowNextFollowDate]    BIT               NOT NULL DEFAULT 1,
        [OutputMode]            NVARCHAR(MAX)     NULL DEFAULT N'Both',
        [ScreenDurationSeconds] INT               NOT NULL DEFAULT 12
    );
END
ELSE
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'OutputMode')
        ALTER TABLE [dbo].[PrintSetting] ADD [OutputMode] NVARCHAR(MAX) NULL DEFAULT N'Both';

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'ScreenDurationSeconds')
        ALTER TABLE [dbo].[PrintSetting] ADD [ScreenDurationSeconds] INT NOT NULL DEFAULT 12;
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[PrintSetting])
BEGIN
    INSERT INTO [dbo].[PrintSetting] (
        [PrinterType], [PrinterName], [PaperWidthMm], [AutoPrintOnAttendance], 
        [PrintCopies], [HeaderText], [FooterText], [ShowBarcode], 
        [ShowNationalId], [ShowNextFollowDate], [OutputMode], [ScreenDurationSeconds]
    ) VALUES (
        N'Thermal', N'', 80, 1, 1, 
        N'حضور متابعة', N'يرجى الالتزام بموعد المتابعة القادم', 1, 1, 1, 
        N'Both', 12
    );
END";
                    cmd.ExecuteNonQuery();
                    _schemaEnsured = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"EnsureTableAndColumnsExist error: {ex.Message}");
                }
            }
        }

        private async Task<List<PrintSetting>> GetAllDataDirectSqlAsync()
        {
            await EnsureTableAndColumnsExistAsync();
            var list = new List<PrintSetting>();
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, PrinterType, PrinterName, PaperWidthMm, PrintCopies, AutoPrintOnAttendance, HeaderText, FooterText, ShowBarcode, ShowNationalId, ShowNextFollowDate, OutputMode, ScreenDurationSeconds FROM PrintSetting ORDER BY Id ASC";
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new PrintSetting
                    {
                        Id = reader.GetInt32(0),
                        PrinterType = reader.IsDBNull(1) ? "Thermal" : reader.GetString(1),
                        PrinterName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        PaperWidthMm = reader.IsDBNull(3) ? 80 : reader.GetInt32(3),
                        PrintCopies = reader.IsDBNull(4) ? 1 : reader.GetInt32(4),
                        AutoPrintOnAttendance = !reader.IsDBNull(5) && reader.GetBoolean(5),
                        HeaderText = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        FooterText = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        ShowBarcode = !reader.IsDBNull(8) && reader.GetBoolean(8),
                        ShowNationalId = !reader.IsDBNull(9) && reader.GetBoolean(9),
                        ShowNextFollowDate = !reader.IsDBNull(10) && reader.GetBoolean(10),
                        OutputMode = reader.IsDBNull(11) ? "Both" : reader.GetString(11),
                        ScreenDurationSeconds = reader.IsDBNull(12) ? 12 : reader.GetInt32(12)
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllDataDirectSqlAsync failed: {ex.Message}");
            }
            return list;
        }

        private List<PrintSetting> GetAllDataDirectSql()
        {
            EnsureTableAndColumnsExist();
            var list = new List<PrintSetting>();
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, PrinterType, PrinterName, PaperWidthMm, PrintCopies, AutoPrintOnAttendance, HeaderText, FooterText, ShowBarcode, ShowNationalId, ShowNextFollowDate, OutputMode, ScreenDurationSeconds FROM PrintSetting ORDER BY Id ASC";
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PrintSetting
                    {
                        Id = reader.GetInt32(0),
                        PrinterType = reader.IsDBNull(1) ? "Thermal" : reader.GetString(1),
                        PrinterName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        PaperWidthMm = reader.IsDBNull(3) ? 80 : reader.GetInt32(3),
                        PrintCopies = reader.IsDBNull(4) ? 1 : reader.GetInt32(4),
                        AutoPrintOnAttendance = !reader.IsDBNull(5) && reader.GetBoolean(5),
                        HeaderText = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        FooterText = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        ShowBarcode = !reader.IsDBNull(8) && reader.GetBoolean(8),
                        ShowNationalId = !reader.IsDBNull(9) && reader.GetBoolean(9),
                        ShowNextFollowDate = !reader.IsDBNull(10) && reader.GetBoolean(10),
                        OutputMode = reader.IsDBNull(11) ? "Both" : reader.GetString(11),
                        ScreenDurationSeconds = reader.IsDBNull(12) ? 12 : reader.GetInt32(12)
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllDataDirectSql failed: {ex.Message}");
            }
            return list;
        }

        private async Task<int> SaveViaDirectSqlAsync(PrintSetting table)
        {
            await EnsureTableAndColumnsExistAsync();
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
IF EXISTS (SELECT 1 FROM PrintSetting WHERE Id = @Id)
BEGIN
    UPDATE PrintSetting SET
        PrinterType = @PrinterType,
        PrinterName = @PrinterName,
        PaperWidthMm = @PaperWidthMm,
        PrintCopies = @PrintCopies,
        AutoPrintOnAttendance = @AutoPrintOnAttendance,
        HeaderText = @HeaderText,
        FooterText = @FooterText,
        ShowBarcode = @ShowBarcode,
        ShowNationalId = @ShowNationalId,
        ShowNextFollowDate = @ShowNextFollowDate,
        OutputMode = @OutputMode,
        ScreenDurationSeconds = @ScreenDurationSeconds
    WHERE Id = @Id;
    SELECT @Id;
END
ELSE IF EXISTS (SELECT 1 FROM PrintSetting)
BEGIN
    DECLARE @FirstId INT;
    SELECT TOP 1 @FirstId = Id FROM PrintSetting ORDER BY Id ASC;
    UPDATE PrintSetting SET
        PrinterType = @PrinterType,
        PrinterName = @PrinterName,
        PaperWidthMm = @PaperWidthMm,
        PrintCopies = @PrintCopies,
        AutoPrintOnAttendance = @AutoPrintOnAttendance,
        HeaderText = @HeaderText,
        FooterText = @FooterText,
        ShowBarcode = @ShowBarcode,
        ShowNationalId = @ShowNationalId,
        ShowNextFollowDate = @ShowNextFollowDate,
        OutputMode = @OutputMode,
        ScreenDurationSeconds = @ScreenDurationSeconds
    WHERE Id = @FirstId;
    SELECT @FirstId;
END
ELSE
BEGIN
    INSERT INTO PrintSetting (
        PrinterType, PrinterName, PaperWidthMm, PrintCopies, AutoPrintOnAttendance,
        HeaderText, FooterText, ShowBarcode, ShowNationalId, ShowNextFollowDate,
        OutputMode, ScreenDurationSeconds
    ) VALUES (
        @PrinterType, @PrinterName, @PaperWidthMm, @PrintCopies, @AutoPrintOnAttendance,
        @HeaderText, @FooterText, @ShowBarcode, @ShowNationalId, @ShowNextFollowDate,
        @OutputMode, @ScreenDurationSeconds
    );
    SELECT SCOPE_IDENTITY();
END";
                cmd.Parameters.AddWithValue("@Id", table.Id);
                cmd.Parameters.AddWithValue("@PrinterType", (object)table.PrinterType ?? "Thermal");
                cmd.Parameters.AddWithValue("@PrinterName", (object)table.PrinterName ?? "");
                cmd.Parameters.AddWithValue("@PaperWidthMm", table.PaperWidthMm > 0 ? table.PaperWidthMm : 80);
                cmd.Parameters.AddWithValue("@PrintCopies", table.PrintCopies > 0 ? table.PrintCopies : 1);
                cmd.Parameters.AddWithValue("@AutoPrintOnAttendance", table.AutoPrintOnAttendance);
                cmd.Parameters.AddWithValue("@HeaderText", (object)table.HeaderText ?? "");
                cmd.Parameters.AddWithValue("@FooterText", (object)table.FooterText ?? "");
                cmd.Parameters.AddWithValue("@ShowBarcode", table.ShowBarcode);
                cmd.Parameters.AddWithValue("@ShowNationalId", table.ShowNationalId);
                cmd.Parameters.AddWithValue("@ShowNextFollowDate", table.ShowNextFollowDate);
                cmd.Parameters.AddWithValue("@OutputMode", (object)table.OutputMode ?? "Both");
                cmd.Parameters.AddWithValue("@ScreenDurationSeconds", table.ScreenDurationSeconds >= 0 ? table.ScreenDurationSeconds : 12);
                var scalar = await cmd.ExecuteScalarAsync();
                if (scalar != null && int.TryParse(scalar.ToString(), out int savedId) && savedId > 0)
                {
                    table.Id = savedId;
                }
                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveViaDirectSqlAsync failed: {ex.Message}");
                return 0;
            }
        }

        private int SaveViaDirectSql(PrintSetting table)
        {
            EnsureTableAndColumnsExist();
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
IF EXISTS (SELECT 1 FROM PrintSetting WHERE Id = @Id)
BEGIN
    UPDATE PrintSetting SET
        PrinterType = @PrinterType,
        PrinterName = @PrinterName,
        PaperWidthMm = @PaperWidthMm,
        PrintCopies = @PrintCopies,
        AutoPrintOnAttendance = @AutoPrintOnAttendance,
        HeaderText = @HeaderText,
        FooterText = @FooterText,
        ShowBarcode = @ShowBarcode,
        ShowNationalId = @ShowNationalId,
        ShowNextFollowDate = @ShowNextFollowDate,
        OutputMode = @OutputMode,
        ScreenDurationSeconds = @ScreenDurationSeconds
    WHERE Id = @Id;
    SELECT @Id;
END
ELSE IF EXISTS (SELECT 1 FROM PrintSetting)
BEGIN
    DECLARE @FirstId INT;
    SELECT TOP 1 @FirstId = Id FROM PrintSetting ORDER BY Id ASC;
    UPDATE PrintSetting SET
        PrinterType = @PrinterType,
        PrinterName = @PrinterName,
        PaperWidthMm = @PaperWidthMm,
        PrintCopies = @PrintCopies,
        AutoPrintOnAttendance = @AutoPrintOnAttendance,
        HeaderText = @HeaderText,
        FooterText = @FooterText,
        ShowBarcode = @ShowBarcode,
        ShowNationalId = @ShowNationalId,
        ShowNextFollowDate = @ShowNextFollowDate,
        OutputMode = @OutputMode,
        ScreenDurationSeconds = @ScreenDurationSeconds
    WHERE Id = @FirstId;
    SELECT @FirstId;
END
ELSE
BEGIN
    INSERT INTO PrintSetting (
        PrinterType, PrinterName, PaperWidthMm, PrintCopies, AutoPrintOnAttendance,
        HeaderText, FooterText, ShowBarcode, ShowNationalId, ShowNextFollowDate,
        OutputMode, ScreenDurationSeconds
    ) VALUES (
        @PrinterType, @PrinterName, @PaperWidthMm, @PrintCopies, @AutoPrintOnAttendance,
        @HeaderText, @FooterText, @ShowBarcode, @ShowNationalId, @ShowNextFollowDate,
        @OutputMode, @ScreenDurationSeconds
    );
    SELECT SCOPE_IDENTITY();
END";
                cmd.Parameters.AddWithValue("@Id", table.Id);
                cmd.Parameters.AddWithValue("@PrinterType", (object)table.PrinterType ?? "Thermal");
                cmd.Parameters.AddWithValue("@PrinterName", (object)table.PrinterName ?? "");
                cmd.Parameters.AddWithValue("@PaperWidthMm", table.PaperWidthMm > 0 ? table.PaperWidthMm : 80);
                cmd.Parameters.AddWithValue("@PrintCopies", table.PrintCopies > 0 ? table.PrintCopies : 1);
                cmd.Parameters.AddWithValue("@AutoPrintOnAttendance", table.AutoPrintOnAttendance);
                cmd.Parameters.AddWithValue("@HeaderText", (object)table.HeaderText ?? "");
                cmd.Parameters.AddWithValue("@FooterText", (object)table.FooterText ?? "");
                cmd.Parameters.AddWithValue("@ShowBarcode", table.ShowBarcode);
                cmd.Parameters.AddWithValue("@ShowNationalId", table.ShowNationalId);
                cmd.Parameters.AddWithValue("@ShowNextFollowDate", table.ShowNextFollowDate);
                cmd.Parameters.AddWithValue("@OutputMode", (object)table.OutputMode ?? "Both");
                cmd.Parameters.AddWithValue("@ScreenDurationSeconds", table.ScreenDurationSeconds >= 0 ? table.ScreenDurationSeconds : 12);
                var scalar = cmd.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int savedId) && savedId > 0)
                {
                    table.Id = savedId;
                }
                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveViaDirectSql failed: {ex.Message}");
                return 0;
            }
        }
        #endregion
    }
}
