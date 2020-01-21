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
                    { 2, "Afghani", 971, "AFN" },
                    { 117, "Rial Omani", 512, "OMR" },
                    { 118, "Pakistan Rupee", 586, "PKR" },
                    { 119, "Balboa", 590, "PAB" },
                    { 120, "Kina", 598, "PGK" },
                    { 121, "Guarani", 600, "PYG" },
                    { 122, "Sol", 604, "PEN" },
                    { 123, "Philippine Peso", 608, "PHP" },
                    { 124, "Zloty", 985, "PLN" },
                    { 116, "Naira", 566, "NGN" },
                    { 125, "Qatari Rial", 634, "QAR" },
                    { 127, "Russian Ruble", 643, "RUB" },
                    { 128, "Rwanda Franc", 646, "RWF" },
                    { 129, "Saint Helena Pound", 654, "SHP" },
                    { 130, "Tala", 882, "WST" },
                    { 131, "Dobra", 930, "STN" },
                    { 132, "Saudi Riyal", 682, "SAR" },
                    { 133, "Serbian Dinar", 941, "RSD" },
                    { 134, "Seychelles Rupee", 690, "SCR" },
                    { 126, "Romanian Leu", 946, "RON" },
                    { 115, "Cordoba Oro", 558, "NIO" },
                    { 114, "Nepalese Rupee", 524, "NPR" },
                    { 113, "Namibia Dollar", 516, "NAD" },
                    { 94, "Liberian Dollar", 430, "LRD" },
                    { 95, "Libyan Dinar", 434, "LYD" },
                    { 96, "Swiss Franc", 756, "CHF" },
                    { 97, "Pataca", 446, "MOP" },
                    { 98, "Denar", 807, "MKD" },
                    { 99, "Malagasy Ariary", 969, "MGA" },
                    { 100, "Malawi Kwacha", 454, "MWK" },
                    { 101, "Malaysian Ringgit", 458, "MYR" },
                    { 102, "Rufiyaa", 462, "MVR" },
                    { 103, "Ouguiya", 929, "MRU" },
                    { 104, "Mauritius Rupee", 480, "MUR" },
                    { 105, "ADB Unit of Account", 965, "XUA" },
                    { 106, "Mexican Peso", 484, "MXN" },
                    { 107, "Mexican Unidad de Inversion (UDI)", 979, "MXV" },
                    { 108, "Moldovan Leu", 498, "MDL" },
                    { 109, "Tugrik", 496, "MNT" },
                    { 110, "Moroccan Dirham", 504, "MAD" },
                    { 111, "Mozambique Metical", 943, "MZN" },
                    { 112, "Kyat", 104, "MMK" },
                    { 135, "Leone", 694, "SLL" },
                    { 93, "Rand", 710, "ZAR" },
                    { 136, "Singapore Dollar", 702, "SGD" },
                    { 138, "Solomon Islands Dollar", 90, "SBD" },
                    { 162, "Peso Uruguayo", 858, "UYU" },
                    { 163, "Uruguay Peso en Unidades Indexadas (UI)", 940, "UYI" },
                    { 164, "Unidad Previsional", 927, "UYW" },
                    { 165, "Uzbekistan Sum", 860, "UZS" },
                    { 166, "Vatu", 548, "VUV" },
                    { 167, "Bolívar Soberano", 928, "VES" },
                    { 168, "Dong", 704, "VND" },
                    { 169, "Yemeni Rial", 886, "YER" },
                    { 161, "US Dollar (Next day)", 997, "USN" },
                    { 170, "Zambian Kwacha", 967, "ZMW" },
                    { 172, "Bond Markets Unit European Composite Unit (EURCO)", 955, "XBA" },
                    { 173, "Bond Markets Unit European Monetary Unit (E.M.U.-6)", 956, "XBB" },
                    { 174, "Bond Markets Unit European Unit of Account 9 (E.U.A.-9)", 957, "XBC" },
                    { 175, "Bond Markets Unit European Unit of Account 17 (E.U.A.-17)", 958, "XBD" },
                    { 176, "Codes specifically reserved for testing purposes", 963, "XTS" },
                    { 177, "The codes assigned for transactions where no currency is involved", 999, "XXX" },
                    { 178, "Gold", 959, "XAU" },
                    { 179, "Palladium", 964, "XPD" },
                    { 171, "Zimbabwe Dollar", 932, "ZWL" },
                    { 160, "UAE Dirham", 784, "AED" },
                    { 159, "Hryvnia", 980, "UAH" },
                    { 158, "Uganda Shilling", 800, "UGX" },
                    { 139, "Somali Shilling", 706, "SOS" },
                    { 140, "South Sudanese Pound", 728, "SSP" },
                    { 141, "Sri Lanka Rupee", 144, "LKR" },
                    { 142, "Sudanese Pound", 938, "SDG" },
                    { 143, "Surinam Dollar", 968, "SRD" },
                    { 144, "Lilangeni", 748, "SZL" },
                    { 145, "Swedish Krona", 752, "SEK" },
                    { 146, "WIR Euro", 947, "CHE" },
                    { 147, "WIR Franc", 948, "CHW" },
                    { 148, "Syrian Pound", 760, "SYP" },
                    { 149, "New Taiwan Dollar", 901, "TWD" },
                    { 150, "Somoni", 972, "TJS" },
                    { 151, "Tanzanian Shilling", 834, "TZS" },
                    { 152, "Baht", 764, "THB" },
                    { 153, "Pa’anga", 776, "TOP" },
                    { 154, "Trinidad and Tobago Dollar", 780, "TTD" },
                    { 155, "Tunisian Dinar", 788, "TND" },
                    { 156, "Turkish Lira", 949, "TRY" },
                    { 157, "Turkmenistan New Manat", 934, "TMT" },
                    { 137, "Sucre", 994, "XSU" },
                    { 180, "Platinum", 962, "XPT" },
                    { 92, "Loti", 426, "LSL" },
                    { 90, "Lao Kip", 418, "LAK" },
                    { 27, "Convertible Mark", 977, "BAM" },
                    { 28, "Pula", 72, "BWP" },
                    { 29, "Norwegian Krone", 578, "NOK" },
                    { 30, "Brazilian Real", 986, "BRL" },
                    { 31, "Brunei Dollar", 96, "BND" },
                    { 32, "Bulgarian Lev", 975, "BGN" },
                    { 33, "Burundi Franc", 108, "BIF" },
                    { 34, "Cabo Verde Escudo", 132, "CVE" },
                    { 26, "Mvdol", 984, "BOV" },
                    { 35, "Riel", 116, "KHR" },
                    { 37, "Canadian Dollar", 124, "CAD" },
                    { 38, "Cayman Islands Dollar", 136, "KYD" },
                    { 39, "Chilean Peso", 152, "CLP" },
                    { 40, "Unidad de Fomento", 990, "CLF" },
                    { 41, "Yuan Renminbi", 156, "CNY" },
                    { 42, "Colombian Peso", 170, "COP" },
                    { 43, "Unidad de Valor Real", 970, "COU" },
                    { 44, "Comorian Franc ", 174, "KMF" },
                    { 36, "CFA Franc BEAC", 950, "XAF" },
                    { 25, "Boliviano", 68, "BOB" },
                    { 24, "Ngultrum", 64, "BTN" },
                    { 23, "Indian Rupee", 356, "INR" },
                    { 3, "Euro", 978, "EUR" },
                    { 4, "Lek", 8, "ALL" },
                    { 5, "Algerian Dinar", 12, "DZD" },
                    { 6, "US Dollar", 840, "USD" },
                    { 7, "Kwanza", 973, "AOA" },
                    { 8, "East Caribbean Dollar", 951, "XCD" },
                    { 10, "Argentine Peso", 32, "ARS" },
                    { 11, "Armenian Dram", 51, "AMD" },
                    { 12, "Aruban Florin", 533, "AWG" },
                    { 13, "Australian Dollar", 36, "AUD" },
                    { 14, "Azerbaijan Manat", 944, "AZN" },
                    { 15, "Bahamian Dollar", 44, "BSD" },
                    { 16, "Bahraini Dinar", 48, "BHD" },
                    { 17, "Taka", 50, "BDT" },
                    { 18, "Barbados Dollar", 52, "BBD" },
                    { 19, "Belarusian Ruble", 933, "BYN" },
                    { 20, "Belize Dollar", 84, "BZD" },
                    { 21, "CFA Franc BCEAO", 952, "XOF" },
                    { 22, "Bermudian Dollar", 60, "BMD" },
                    { 45, "Congolese Franc", 976, "CDF" },
                    { 91, "Lebanese Pound", 422, "LBP" },
                    { 46, "New Zealand Dollar", 554, "NZD" },
                    { 48, "Kuna", 191, "HRK" },
                    { 72, "Lempira", 340, "HNL" },
                    { 73, "Hong Kong Dollar", 344, "HKD" },
                    { 74, "Forint", 348, "HUF" },
                    { 75, "Iceland Krona", 352, "ISK" },
                    { 76, "Rupiah", 360, "IDR" },
                    { 77, "SDR (Special Drawing Right)", 960, "XDR" },
                    { 78, "Iranian Rial", 364, "IRR" },
                    { 79, "Iraqi Dinar", 368, "IQD" },
                    { 71, "Gourde", 332, "HTG" },
                    { 80, "New Israeli Sheqel", 376, "ILS" },
                    { 82, "Yen", 392, "JPY" },
                    { 83, "Jordanian Dinar", 400, "JOD" },
                    { 84, "Tenge", 398, "KZT" },
                    { 85, "Kenyan Shilling", 404, "KES" },
                    { 86, "North Korean Won", 408, "KPW" },
                    { 87, "Won", 410, "KRW" },
                    { 88, "Kuwaiti Dinar", 414, "KWD" },
                    { 89, "Som", 417, "KGS" },
                    { 81, "Jamaican Dollar", 388, "JMD" },
                    { 70, "Guyana Dollar", 328, "GYD" },
                    { 69, "Guinean Franc", 324, "GNF" },
                    { 68, "Pound Sterling", 826, "GBP" },
                    { 49, "Cuban Peso", 192, "CUP" },
                    { 50, "Peso Convertible", 931, "CUC" },
                    { 51, "Netherlands Antillean Guilder", 532, "ANG" },
                    { 52, "Czech Koruna", 203, "CZK" },
                    { 53, "Danish Krone", 208, "DKK" },
                    { 54, "Djibouti Franc", 262, "DJF" },
                    { 55, "Dominican Peso", 214, "DOP" },
                    { 56, "Egyptian Pound", 818, "EGP" },
                    { 57, "El Salvador Colon", 222, "SVC" },
                    { 58, "Nakfa", 232, "ERN" },
                    { 59, "Ethiopian Birr", 230, "ETB" },
                    { 60, "Falkland Islands Pound", 238, "FKP" },
                    { 61, "Fiji Dollar", 242, "FJD" },
                    { 62, "CFP Franc", 953, "XPF" },
                    { 63, "Dalasi", 270, "GMD" },
                    { 64, "Lari", 981, "GEL" },
                    { 65, "Ghana Cedi", 936, "GHS" },
                    { 66, "Gibraltar Pound", 292, "GIP" },
                    { 67, "Quetzal", 320, "GTQ" },
                    { 47, "Costa Rican Colon", 188, "CRC" },
                    { 181, "Silver", 961, "XAG" }
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
