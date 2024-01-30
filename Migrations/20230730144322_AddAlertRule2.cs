using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertRule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertTemplates",
                columns: table => new
                {
                    AlertTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertTemplates", x => x.AlertTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "AlertRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operator = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertTemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertRules_AlertTemplates_AlertTemplateId",
                        column: x => x.AlertTemplateId,
                        principalTable: "AlertTemplates",
                        principalColumn: "AlertTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertRules_AlertTemplateId",
                table: "AlertRules",
                column: "AlertTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertRules");

            migrationBuilder.DropTable(
                name: "AlertTemplates");
        }
    }
}
