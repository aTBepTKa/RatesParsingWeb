using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class SeedCommandAssignmentPLN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TextCodeCommandAssignments",
                columns: new[] { "Id", "CommandId", "ParsingSettingsId" },
                values: new object[] { 1, 2, 2 });

            migrationBuilder.InsertData(
                table: "UnitCommandAssignments",
                columns: new[] { "Id", "CommandId", "ParsingSettingsId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "TextCodeCommandParameters",
                columns: new[] { "Id", "TextCodeCommandAssignmentId", "Value" },
                values: new object[] { 1, 1, "3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TextCodeCommandParameters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UnitCommandAssignments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TextCodeCommandAssignments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
