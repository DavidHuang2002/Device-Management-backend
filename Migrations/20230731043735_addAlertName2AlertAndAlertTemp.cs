using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class addAlertName2AlertAndAlertTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlertName",
                table: "AlertTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AlertName",
                table: "Alert",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertName",
                table: "AlertTemplates");

            migrationBuilder.DropColumn(
                name: "AlertName",
                table: "Alert");
        }
    }
}
