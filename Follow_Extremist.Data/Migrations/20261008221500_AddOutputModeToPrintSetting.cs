using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Follow_Extremist.Data.Migrations
{
    public partial class AddOutputModeToPrintSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OutputMode",
                table: "PrintSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Both");

            migrationBuilder.AddColumn<int>(
                name: "ScreenDurationSeconds",
                table: "PrintSetting",
                type: "int",
                nullable: false,
                defaultValue: 12);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutputMode",
                table: "PrintSetting");

            migrationBuilder.DropColumn(
                name: "ScreenDurationSeconds",
                table: "PrintSetting");
        }
    }
}
