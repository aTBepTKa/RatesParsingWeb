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
                    Name = table.Column<string>(maxLength: 50, nullable: true),
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
                    CurrencyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
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
                    CurrencyID = table.Column<int>(nullable: false),
                    ExchangeRateListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
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

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CurrencyID",
                table: "Banks",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateLists_BankId",
                table: "ExchangeRateLists",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyID",
                table: "ExchangeRates",
                column: "CurrencyID");

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
