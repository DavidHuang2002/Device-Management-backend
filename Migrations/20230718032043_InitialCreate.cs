using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AddedDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastCheckInTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AzureDeviceId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AzureDeviceKey = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AzureConnectionString = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
