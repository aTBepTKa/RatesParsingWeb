using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    TextCode = table.Column<string>(nullable: false),
                    NumCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParamsNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BankUrl = table.Column<string>(maxLength: 2000, nullable: true),
                    RatesUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRateLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    BankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRateLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRateLists_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParsingSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextCodeXpath = table.Column<string>(maxLength: 2000, nullable: true),
                    UnitXpath = table.Column<string>(maxLength: 2000, nullable: true),
                    ExchangeRateXpath = table.Column<string>(maxLength: 2000, nullable: true),
                    VariablePartOfXpath = table.Column<string>(maxLength: 50, nullable: true),
                    StartXpathRow = table.Column<int>(nullable: true),
                    EndXpathRow = table.Column<int>(nullable: true),
                    NumberDecimalSeparator = table.Column<string>(nullable: false),
                    NumberGroupSeparator = table.Column<string>(nullable: false),
                    BankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsingSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsingSettings_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<int>(nullable: false),
                    ExchangeRateValue = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    ExchangeRateListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_ExchangeRateLists_ExchangeRateListId",
                        column: x => x.ExchangeRateListId,
                        principalTable: "ExchangeRateLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextCodeScriptAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptId = table.Column<int>(nullable: false),
                    BankId = table.Column<int>(nullable: false),
                    ParsingSettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCodeScriptAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCodeScriptAssignments_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextCodeScriptAssignments_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitScriptAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScriptId = table.Column<int>(nullable: false),
                    BankId = table.Column<int>(nullable: false),
                    ParsingSettingsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitScriptAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitScriptAssignments_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitScriptAssignments_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextCodeScriptParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 50, nullable: false),
                    TextCodeScriptAssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCodeScriptParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCodeScriptParameters_TextCodeScriptAssignments_TextCodeScriptAssignmentId",
                        column: x => x.TextCodeScriptAssignmentId,
                        principalTable: "TextCodeScriptAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitScriptParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 50, nullable: false),
                    UnitScriptAssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitScriptParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitScriptParameters_UnitScriptAssignments_UnitScriptAssignmentId",
                        column: x => x.UnitScriptAssignmentId,
                        principalTable: "UnitScriptAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "NumCode", "TextCode" },
                values: new object[,]
                {
                    { 971, "Afghani", null, "AFN" },
                    { 512, "Rial Omani", null, "OMR" },
                    { 586, "Pakistan Rupee", null, "PKR" },
                    { 590, "Balboa", null, "PAB" },
                    { 598, "Kina", null, "PGK" },
                    { 600, "Guarani", null, "PYG" },
                    { 604, "Sol", null, "PEN" },
                    { 608, "Philippine Peso", null, "PHP" },
                    { 985, "Zloty", null, "PLN" },
                    { 566, "Naira", null, "NGN" },
                    { 634, "Qatari Rial", null, "QAR" },
                    { 643, "Russian Ruble", null, "RUB" },
                    { 646, "Rwanda Franc", null, "RWF" },
                    { 654, "Saint Helena Pound", null, "SHP" },
                    { 882, "Tala", null, "WST" },
                    { 930, "Dobra", null, "STN" },
                    { 682, "Saudi Riyal", null, "SAR" },
                    { 941, "Serbian Dinar", null, "RSD" },
                    { 690, "Seychelles Rupee", null, "SCR" },
                    { 946, "Romanian Leu", null, "RON" },
                    { 558, "Cordoba Oro", null, "NIO" },
                    { 524, "Nepalese Rupee", null, "NPR" },
                    { 516, "Namibia Dollar", null, "NAD" },
                    { 430, "Liberian Dollar", null, "LRD" },
                    { 434, "Libyan Dinar", null, "LYD" },
                    { 756, "Swiss Franc", null, "CHF" },
                    { 446, "Pataca", null, "MOP" },
                    { 807, "Denar", null, "MKD" },
                    { 969, "Malagasy Ariary", null, "MGA" },
                    { 454, "Malawi Kwacha", null, "MWK" },
                    { 458, "Malaysian Ringgit", null, "MYR" },
                    { 462, "Rufiyaa", null, "MVR" },
                    { 929, "Ouguiya", null, "MRU" },
                    { 480, "Mauritius Rupee", null, "MUR" },
                    { 965, "ADB Unit of Account", null, "XUA" },
                    { 484, "Mexican Peso", null, "MXN" },
                    { 979, "Mexican Unidad de Inversion (UDI)", null, "MXV" },
                    { 498, "Moldovan Leu", null, "MDL" },
                    { 496, "Tugrik", null, "MNT" },
                    { 504, "Moroccan Dirham", null, "MAD" },
                    { 943, "Mozambique Metical", null, "MZN" },
                    { 104, "Kyat", null, "MMK" },
                    { 694, "Leone", null, "SLL" },
                    { 710, "Rand", null, "ZAR" },
                    { 702, "Singapore Dollar", null, "SGD" },
                    { 90, "Solomon Islands Dollar", null, "SBD" },
                    { 858, "Peso Uruguayo", null, "UYU" },
                    { 940, "Uruguay Peso en Unidades Indexadas (UI)", null, "UYI" },
                    { 927, "Unidad Previsional", null, "UYW" },
                    { 860, "Uzbekistan Sum", null, "UZS" },
                    { 548, "Vatu", null, "VUV" },
                    { 928, "Bolívar Soberano", null, "VES" },
                    { 704, "Dong", null, "VND" },
                    { 886, "Yemeni Rial", null, "YER" },
                    { 997, "US Dollar (Next day)", null, "USN" },
                    { 967, "Zambian Kwacha", null, "ZMW" },
                    { 955, "Bond Markets Unit European Composite Unit (EURCO)", null, "XBA" },
                    { 956, "Bond Markets Unit European Monetary Unit (E.M.U.-6)", null, "XBB" },
                    { 957, "Bond Markets Unit European Unit of Account 9 (E.U.A.-9)", null, "XBC" },
                    { 958, "Bond Markets Unit European Unit of Account 17 (E.U.A.-17)", null, "XBD" },
                    { 963, "Codes specifically reserved for testing purposes", null, "XTS" },
                    { 999, "The codes assigned for transactions where no currency is involved", null, "XXX" },
                    { 959, "Gold", null, "XAU" },
                    { 964, "Palladium", null, "XPD" },
                    { 932, "Zimbabwe Dollar", null, "ZWL" },
                    { 784, "UAE Dirham", null, "AED" },
                    { 980, "Hryvnia", null, "UAH" },
                    { 800, "Uganda Shilling", null, "UGX" },
                    { 706, "Somali Shilling", null, "SOS" },
                    { 728, "South Sudanese Pound", null, "SSP" },
                    { 144, "Sri Lanka Rupee", null, "LKR" },
                    { 938, "Sudanese Pound", null, "SDG" },
                    { 968, "Surinam Dollar", null, "SRD" },
                    { 748, "Lilangeni", null, "SZL" },
                    { 752, "Swedish Krona", null, "SEK" },
                    { 947, "WIR Euro", null, "CHE" },
                    { 948, "WIR Franc", null, "CHW" },
                    { 760, "Syrian Pound", null, "SYP" },
                    { 901, "New Taiwan Dollar", null, "TWD" },
                    { 972, "Somoni", null, "TJS" },
                    { 834, "Tanzanian Shilling", null, "TZS" },
                    { 764, "Baht", null, "THB" },
                    { 776, "Pa’anga", null, "TOP" },
                    { 780, "Trinidad and Tobago Dollar", null, "TTD" },
                    { 788, "Tunisian Dinar", null, "TND" },
                    { 949, "Turkish Lira", null, "TRY" },
                    { 934, "Turkmenistan New Manat", null, "TMT" },
                    { 994, "Sucre", null, "XSU" },
                    { 962, "Platinum", null, "XPT" },
                    { 426, "Loti", null, "LSL" },
                    { 418, "Lao Kip", null, "LAK" },
                    { 977, "Convertible Mark", null, "BAM" },
                    { 72, "Pula", null, "BWP" },
                    { 578, "Norwegian Krone", null, "NOK" },
                    { 986, "Brazilian Real", null, "BRL" },
                    { 96, "Brunei Dollar", null, "BND" },
                    { 975, "Bulgarian Lev", null, "BGN" },
                    { 108, "Burundi Franc", null, "BIF" },
                    { 132, "Cabo Verde Escudo", null, "CVE" },
                    { 984, "Mvdol", null, "BOV" },
                    { 116, "Riel", null, "KHR" },
                    { 124, "Canadian Dollar", null, "CAD" },
                    { 136, "Cayman Islands Dollar", null, "KYD" },
                    { 152, "Chilean Peso", null, "CLP" },
                    { 990, "Unidad de Fomento", null, "CLF" },
                    { 156, "Yuan Renminbi", null, "CNY" },
                    { 170, "Colombian Peso", null, "COP" },
                    { 970, "Unidad de Valor Real", null, "COU" },
                    { 174, "Comorian Franc ", null, "KMF" },
                    { 950, "CFA Franc BEAC", null, "XAF" },
                    { 68, "Boliviano", null, "BOB" },
                    { 64, "Ngultrum", null, "BTN" },
                    { 356, "Indian Rupee", null, "INR" },
                    { 978, "Euro", null, "EUR" },
                    { 8, "Lek", null, "ALL" },
                    { 12, "Algerian Dinar", null, "DZD" },
                    { 840, "US Dollar", null, "USD" },
                    { 973, "Kwanza", null, "AOA" },
                    { 951, "East Caribbean Dollar", null, "XCD" },
                    { 32, "Argentine Peso", null, "ARS" },
                    { 51, "Armenian Dram", null, "AMD" },
                    { 533, "Aruban Florin", null, "AWG" },
                    { 36, "Australian Dollar", null, "AUD" },
                    { 944, "Azerbaijan Manat", null, "AZN" },
                    { 44, "Bahamian Dollar", null, "BSD" },
                    { 48, "Bahraini Dinar", null, "BHD" },
                    { 50, "Taka", null, "BDT" },
                    { 52, "Barbados Dollar", null, "BBD" },
                    { 933, "Belarusian Ruble", null, "BYN" },
                    { 84, "Belize Dollar", null, "BZD" },
                    { 952, "CFA Franc BCEAO", null, "XOF" },
                    { 60, "Bermudian Dollar", null, "BMD" },
                    { 976, "Congolese Franc", null, "CDF" },
                    { 422, "Lebanese Pound", null, "LBP" },
                    { 554, "New Zealand Dollar", null, "NZD" },
                    { 191, "Kuna", null, "HRK" },
                    { 340, "Lempira", null, "HNL" },
                    { 344, "Hong Kong Dollar", null, "HKD" },
                    { 348, "Forint", null, "HUF" },
                    { 352, "Iceland Krona", null, "ISK" },
                    { 360, "Rupiah", null, "IDR" },
                    { 960, "SDR (Special Drawing Right)", null, "XDR" },
                    { 364, "Iranian Rial", null, "IRR" },
                    { 368, "Iraqi Dinar", null, "IQD" },
                    { 332, "Gourde", null, "HTG" },
                    { 376, "New Israeli Sheqel", null, "ILS" },
                    { 392, "Yen", null, "JPY" },
                    { 400, "Jordanian Dinar", null, "JOD" },
                    { 398, "Tenge", null, "KZT" },
                    { 404, "Kenyan Shilling", null, "KES" },
                    { 408, "North Korean Won", null, "KPW" },
                    { 410, "Won", null, "KRW" },
                    { 414, "Kuwaiti Dinar", null, "KWD" },
                    { 417, "Som", null, "KGS" },
                    { 388, "Jamaican Dollar", null, "JMD" },
                    { 328, "Guyana Dollar", null, "GYD" },
                    { 324, "Guinean Franc", null, "GNF" },
                    { 826, "Pound Sterling", null, "GBP" },
                    { 192, "Cuban Peso", null, "CUP" },
                    { 931, "Peso Convertible", null, "CUC" },
                    { 532, "Netherlands Antillean Guilder", null, "ANG" },
                    { 203, "Czech Koruna", null, "CZK" },
                    { 208, "Danish Krone", null, "DKK" },
                    { 262, "Djibouti Franc", null, "DJF" },
                    { 214, "Dominican Peso", null, "DOP" },
                    { 818, "Egyptian Pound", null, "EGP" },
                    { 222, "El Salvador Colon", null, "SVC" },
                    { 232, "Nakfa", null, "ERN" },
                    { 230, "Ethiopian Birr", null, "ETB" },
                    { 238, "Falkland Islands Pound", null, "FKP" },
                    { 242, "Fiji Dollar", null, "FJD" },
                    { 953, "CFP Franc", null, "XPF" },
                    { 270, "Dalasi", null, "GMD" },
                    { 981, "Lari", null, "GEL" },
                    { 936, "Ghana Cedi", null, "GHS" },
                    { 292, "Gibraltar Pound", null, "GIP" },
                    { 320, "Quetzal", null, "GTQ" },
                    { 188, "Costa Rican Colon", null, "CRC" },
                    { 961, "Silver", null, "XAG" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "BankUrl", "CurrencyId", "Name", "RatesUrl" },
                values: new object[,]
                {
                    { 4, null, 978, "European Central Bank", "https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html" },
                    { 1, null, 981, "National Bank of Georgia", "https://www.nbg.gov.ge/index.php?m=582&lng=eng" },
                    { 2, null, 985, "National Bank of Poland", "https://www.nbp.pl/homen.aspx?f=/kursy/RatesA.html" },
                    { 3, null, 643, "The Central Bank of the Russian Federation", "https://www.cbr.ru/eng/currency_base/daily/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CurrencyId",
                table: "Banks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateLists_BankId",
                table: "ExchangeRateLists",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ExchangeRateListId",
                table: "ExchangeRates",
                column: "ExchangeRateListId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsingSettings_BankId",
                table: "ParsingSettings",
                column: "BankId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeScriptAssignments_BankId",
                table: "TextCodeScriptAssignments",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeScriptAssignments_ParsingSettingsId",
                table: "TextCodeScriptAssignments",
                column: "ParsingSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeScriptAssignments_ScriptId",
                table: "TextCodeScriptAssignments",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeScriptParameters_TextCodeScriptAssignmentId",
                table: "TextCodeScriptParameters",
                column: "TextCodeScriptAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitScriptAssignments_BankId",
                table: "UnitScriptAssignments",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitScriptAssignments_ParsingSettingsId",
                table: "UnitScriptAssignments",
                column: "ParsingSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitScriptAssignments_ScriptId",
                table: "UnitScriptAssignments",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitScriptParameters_UnitScriptAssignmentId",
                table: "UnitScriptParameters",
                column: "UnitScriptAssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "TextCodeScriptParameters");

            migrationBuilder.DropTable(
                name: "UnitScriptParameters");

            migrationBuilder.DropTable(
                name: "ExchangeRateLists");

            migrationBuilder.DropTable(
                name: "TextCodeScriptAssignments");

            migrationBuilder.DropTable(
                name: "UnitScriptAssignments");

            migrationBuilder.DropTable(
                name: "ParsingSettings");

            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
