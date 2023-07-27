using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddRaspberryPiUpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Alert",
            //    columns: table => new
            //    {
            //        AlertId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DeviceId = table.Column<int>(type: "int", nullable: false),
            //        Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Severity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        AcknowledgedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        AcknowledgedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ResolvedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        ResolvedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Alert", x => x.AlertId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RaspberryPi",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Temperature = table.Column<double>(type: "float", nullable: true),
            //        Humidity = table.Column<double>(type: "float", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RaspberryPi", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RaspberryPi_Device_Id",
            //            column: x => x.Id,
            //            principalTable: "Device",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "RaspberryPiUpdate",
                columns: table => new
                {
                    UpdateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<float>(type: "real", nullable: true),
                    Humidity = table.Column<float>(type: "real", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaspberryPiUpdate", x => x.UpdateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "RaspberryPi");

            migrationBuilder.DropTable(
                name: "RaspberryPiUpdate");
        }
    }
}
