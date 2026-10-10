using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Follow_Extremist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceEnrollIdAndHardwareFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[FingerprintDevice]') AND name = N'SerialNumber')
BEGIN
    ALTER TABLE [dbo].[FingerprintDevice] ADD [SerialNumber] nvarchar(max) NULL;
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[ElementInfo]') AND name = N'DeviceEnrollId')
BEGIN
    ALTER TABLE [dbo].[ElementInfo] ADD [DeviceEnrollId] int NULL;
END
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ElementInfo_DeviceEnrollId' AND object_id = OBJECT_ID(N'[dbo].[ElementInfo]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ElementInfo_DeviceEnrollId] ON [dbo].[ElementInfo] ([DeviceEnrollId]);
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceEnrollId')
BEGIN
    ALTER TABLE [dbo].[AttendanceLog] ADD [DeviceEnrollId] int NULL;
END
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_AttendanceLog_DeviceEnrollId' AND object_id = OBJECT_ID(N'[dbo].[AttendanceLog]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_DeviceEnrollId] ON [dbo].[AttendanceLog] ([DeviceEnrollId]);
END");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceSerialNumber')
BEGIN
    ALTER TABLE [dbo].[AttendanceLog] ADD [DeviceSerialNumber] nvarchar(max) NULL;
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[FingerprintDevice]') AND name = N'SerialNumber')
BEGIN
    ALTER TABLE [dbo].[FingerprintDevice] DROP COLUMN [SerialNumber];
END");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[ElementInfo]') AND name = N'DeviceEnrollId')
BEGIN
    ALTER TABLE [dbo].[ElementInfo] DROP COLUMN [DeviceEnrollId];
END");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceEnrollId')
BEGIN
    ALTER TABLE [dbo].[AttendanceLog] DROP COLUMN [DeviceEnrollId];
END");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceSerialNumber')
BEGIN
    ALTER TABLE [dbo].[AttendanceLog] DROP COLUMN [DeviceSerialNumber];
END");
        }
    }
}
