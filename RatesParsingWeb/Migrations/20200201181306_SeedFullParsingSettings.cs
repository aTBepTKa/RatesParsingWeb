using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class SeedFullParsingSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartXpathRow",
                table: "ParsingSettings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndXpathRow",
                table: "ParsingSettings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndXpathRow", "ExchangeRateXpath", "StartXpathRow", "TextCodeXpath", "UnitXpath" },
                values: new object[] { 43, "//*[@id='currency_id']/table/tr[$VARIABLE]/td[3]", 1, "//*[@id='currency_id']/table/tr[$VARIABLE]/td[1]", "//*[@id='currency_id']/table/tr[$VARIABLE]/td[2]" });

            migrationBuilder.InsertData(
                table: "ParsingSettings",
                columns: new[] { "Id", "BankId", "EndXpathRow", "ExchangeRateXpath", "NumberDecimalSeparator", "NumberGroupSeparator", "StartXpathRow", "TextCodeXpath", "UnitXpath", "VariablePartOfXpath" },
                values: new object[,]
                {
                    { 2, 2, 36, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[3]", ".", ",", 2, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]", "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]", "$VARIABLE" },
                    { 3, 3, 35, "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[5]", ".", ",", 2, "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[2]", "//*[@id='content']/table/tbody/tr[$VARIABLE]/td[3]", "$VARIABLE" },
                    { 4, 4, 32, "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[3]", ".", ",", 1, "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[1]", "//*[@id='ecb-content-col']/main/div/table/tbody/tr[$VARIABLE]/td[1]", "$VARIABLE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_Name",
                table: "Scripts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_TextCode",
                table: "Currencies",
                column: "TextCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_Name",
                table: "Banks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_SwiftCode",
                table: "Banks",
                column: "SwiftCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Scripts_Name",
                table: "Scripts");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_TextCode",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Banks_Name",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_SwiftCode",
                table: "Banks");

            migrationBuilder.DeleteData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "StartXpathRow",
                table: "ParsingSettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EndXpathRow",
                table: "ParsingSettings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "ParsingSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndXpathRow", "ExchangeRateXpath", "StartXpathRow", "TextCodeXpath", "UnitXpath" },
                values: new object[] { 36, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[3]", 2, "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]", "//*[@id='article']/table/tr/td/center/table[1]/tr[$VARIABLE]/td[2]" });
        }
    }
}
