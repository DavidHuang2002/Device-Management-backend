using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class addDeviceId2AlertRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_Device_DeviceId",
                table: "AlertRules");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "AlertRules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_Device_DeviceId",
                table: "AlertRules",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_Device_DeviceId",
                table: "AlertRules");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "AlertRules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_Device_DeviceId",
                table: "AlertRules",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id");
        }
    }
}
