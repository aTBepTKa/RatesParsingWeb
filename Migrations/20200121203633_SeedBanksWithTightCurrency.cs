using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class SeedBanksWithTightCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "BankUrl", "CurrencyId", "Name", "RatesUrl" },
                values: new object[,]
                {
                    { 1, null, 64, "National Bank of Georgia", "https://www.nbg.gov.ge/index.php?m=582&lng=eng" },
                    { 2, null, 124, "National Bank of Poland", "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html" },
                    { 3, null, 127, "The Central Bank of the Russian Federation", "https://www.cbr.ru/eng/currency_base/daily/" },
                    { 4, null, 3, "European Central Bank", "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
