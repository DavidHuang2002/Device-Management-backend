using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Device_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeDataTypeToAlertRUle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeDataType",
                table: "AlertRules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeDataType",
                table: "AlertRules");
        }
    }
}
