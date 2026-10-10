using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Follow_Extremist.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFingerprintAttendanceSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create FingerprintDevice Table
            migrationBuilder.CreateTable(
                name: "FingerprintDevice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    MachineNumber = table.Column<int>(type: "int", nullable: false),
                    CommPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LastSyncTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FingerprintDevice", x => x.Id);
                });

            // 2. Create PrintSetting Table
            migrationBuilder.CreateTable(
                name: "PrintSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrinterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaperWidthMm = table.Column<int>(type: "int", nullable: false),
                    AutoPrintOnAttendance = table.Column<bool>(type: "bit", nullable: false),
                    PrintCopies = table.Column<int>(type: "int", nullable: false),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowBarcode = table.Column<bool>(type: "bit", nullable: false),
                    ShowNationalId = table.Column<bool>(type: "bit", nullable: false),
                    ShowNextFollowDate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintSetting", x => x.Id);
                });

            // 3. Create ElementFingerprint Table
            migrationBuilder.CreateTable(
                name: "ElementFingerprint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    FingerIndex = table.Column<int>(type: "int", nullable: false),
                    FingerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateVersion = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementFingerprint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementFingerprint_ElementInfo_ElementId",
                        column: x => x.ElementId,
                        principalTable: "ElementInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // 4. Create ElementWantedStatus Table
            migrationBuilder.CreateTable(
                name: "ElementWantedStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    IsWanted = table.Column<bool>(type: "bit", nullable: false),
                    WantedReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WantedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WantedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementWantedStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementWantedStatus_ElementInfo_ElementId",
                        column: x => x.ElementId,
                        principalTable: "ElementInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // 5. Create AttendanceLog Table
            migrationBuilder.CreateTable(
                name: "AttendanceLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: true),
                    AttendanceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifyType = table.Column<int>(type: "int", nullable: false),
                    IsWantedAtTime = table.Column<bool>(type: "bit", nullable: false),
                    NextFollowDateAssigned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: false),
                    PrintedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrintType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceLog_ElementInfo_ElementId",
                        column: x => x.ElementId,
                        principalTable: "ElementInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceLog_FingerprintDevice_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "FingerprintDevice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            // Indexes
            migrationBuilder.CreateIndex(
                name: "IX_AttendanceLog_DeviceId",
                table: "AttendanceLog",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceLog_ElementId",
                table: "AttendanceLog",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementFingerprint_ElementId",
                table: "ElementFingerprint",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementWantedStatus_ElementId",
                table: "ElementWantedStatus",
                column: "ElementId",
                unique: true);

            // Seed default print settings
            migrationBuilder.InsertData(
                table: "PrintSetting",
                columns: new[] { "Id", "PrinterType", "PrinterName", "PaperWidthMm", "AutoPrintOnAttendance", "PrintCopies", "HeaderText", "FooterText", "ShowBarcode", "ShowNationalId", "ShowNextFollowDate" },
                values: new object[] { 1, "Thermal", "", 80, true, 1, "حضور متابعة", "يرجى الالتزام بموعد المتابعة القادم", true, true, true });

            // Create View for SQL Server 2012+ compatibility
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.vw_TodayAttendance', 'V') IS NOT NULL
                    DROP VIEW [dbo].[vw_TodayAttendance];
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW [dbo].[vw_TodayAttendance] AS
                SELECT 
                    a.Id AS AttendanceLogId,
                    a.ElementId,
                    e.ElementName,
                    e.NationalId,
                    a.AttendanceDateTime,
                    ISNULL(d.DeviceName, N'يدوي / غير محدد') AS DeviceName,
                    ISNULL(w.IsWanted, 0) AS IsWanted,
                    a.NextFollowDateAssigned AS NextFollowDate,
                    a.IsPrinted,
                    a.Status
                FROM AttendanceLog a
                INNER JOIN ElementInfo e ON a.ElementId = e.Id
                LEFT JOIN FingerprintDevice d ON a.DeviceId = d.Id
                LEFT JOIN ElementWantedStatus w ON a.ElementId = w.ElementId
                WHERE CAST(a.AttendanceDateTime AS DATE) = CAST(GETDATE() AS DATE);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('dbo.vw_TodayAttendance', 'V') IS NOT NULL
                    DROP VIEW [dbo].[vw_TodayAttendance];
            ");

            migrationBuilder.DropTable(
                name: "AttendanceLog");

            migrationBuilder.DropTable(
                name: "ElementWantedStatus");

            migrationBuilder.DropTable(
                name: "ElementFingerprint");

            migrationBuilder.DropTable(
                name: "PrintSetting");

            migrationBuilder.DropTable(
                name: "FingerprintDevice");
        }
    }
}
