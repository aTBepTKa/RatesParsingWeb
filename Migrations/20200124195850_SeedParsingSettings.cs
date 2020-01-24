using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class SeedParsingSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParsingSettings",
                columns: new[] { "Id", "BankId", "EndXpathRow", "ExchangeRateXpath", "NumberDecimalSeparator", "NumberGroupSeparator", "StartXpathRow", "TextCodeXpath", "UnitXpath", "VariablePartOfXpath" },
                values: new object[] { 1, 1, 36, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[3]", ".", ",", 2, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]", "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]", "$VARIABLE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
