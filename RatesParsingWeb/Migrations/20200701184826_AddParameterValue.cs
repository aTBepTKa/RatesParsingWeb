using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class AddParameterValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: "3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: null);
        }
    }
}
