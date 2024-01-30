using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertRule2Device : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertRuleId",
                table: "Device",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_AlertRuleId",
                table: "Device",
                column: "AlertRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_AlertRules_AlertRuleId",
                table: "Device",
                column: "AlertRuleId",
                principalTable: "AlertRules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_AlertRules_AlertRuleId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_AlertRuleId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "AlertRuleId",
                table: "Device");
        }
    }
}
