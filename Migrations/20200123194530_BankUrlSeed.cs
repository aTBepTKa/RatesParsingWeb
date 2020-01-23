using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class BankUrlSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankUrl",
                value: "https://www.nbg.gov.ge");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankUrl",
                value: "https://www.nbp.pl");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3,
                column: "BankUrl",
                value: "https://www.cbr.ru");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 4,
                column: "BankUrl",
                value: "https://www.ecb.europa.eu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3,
                column: "BankUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 4,
                column: "BankUrl",
                value: null);
        }
    }
}
