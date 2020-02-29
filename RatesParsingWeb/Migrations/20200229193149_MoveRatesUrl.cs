using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class MoveRatesUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatesUrl",
                table: "Banks");

            migrationBuilder.AddColumn<string>(
                name: "RatesUrl",
                table: "ParsingSettings",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "RatesUrl",
                value: "https://www.nbg.gov.ge/index.php?m=582&lng=eng");

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "RatesUrl",
                value: "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html");

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "RatesUrl",
                value: "https://www.cbr.ru/eng/currency_base/daily/");

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "RatesUrl",
                value: "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatesUrl",
                table: "ParsingSettings");

            migrationBuilder.AddColumn<string>(
                name: "RatesUrl",
                table: "Banks",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1,
                column: "RatesUrl",
                value: "https://www.nbg.gov.ge/index.php?m=582&lng=eng");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2,
                column: "RatesUrl",
                value: "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3,
                column: "RatesUrl",
                value: "https://www.cbr.ru/eng/currency_base/daily/");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 4,
                column: "RatesUrl",
                value: "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html");
        }
    }
}
