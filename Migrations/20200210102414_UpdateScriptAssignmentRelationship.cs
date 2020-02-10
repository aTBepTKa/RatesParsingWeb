using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class UpdateScriptAssignmentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextCodeScriptAssignments_Banks_BankId",
                table: "TextCodeScriptAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "TextCodeScriptAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitScriptAssignments_Banks_BankId",
                table: "UnitScriptAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "UnitScriptAssignments");

            migrationBuilder.DropIndex(
                name: "IX_UnitScriptAssignments_BankId",
                table: "UnitScriptAssignments");

            migrationBuilder.DropIndex(
                name: "IX_TextCodeScriptAssignments_BankId",
                table: "TextCodeScriptAssignments");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "UnitScriptAssignments");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "TextCodeScriptAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "ParsingSettingsId",
                table: "UnitScriptAssignments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParsingSettingsId",
                table: "TextCodeScriptAssignments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "TextCodeScriptAssignments",
                column: "ParsingSettingsId",
                principalTable: "ParsingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "UnitScriptAssignments",
                column: "ParsingSettingsId",
                principalTable: "ParsingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "TextCodeScriptAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "UnitScriptAssignments");

            migrationBuilder.AlterColumn<int>(
                name: "ParsingSettingsId",
                table: "UnitScriptAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "UnitScriptAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ParsingSettingsId",
                table: "TextCodeScriptAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "TextCodeScriptAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnitScriptAssignments_BankId",
                table: "UnitScriptAssignments",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeScriptAssignments_BankId",
                table: "TextCodeScriptAssignments",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextCodeScriptAssignments_Banks_BankId",
                table: "TextCodeScriptAssignments",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "TextCodeScriptAssignments",
                column: "ParsingSettingsId",
                principalTable: "ParsingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitScriptAssignments_Banks_BankId",
                table: "UnitScriptAssignments",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                table: "UnitScriptAssignments",
                column: "ParsingSettingsId",
                principalTable: "ParsingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
