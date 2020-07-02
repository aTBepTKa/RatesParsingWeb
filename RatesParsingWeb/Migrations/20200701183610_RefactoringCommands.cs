using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class RefactoringCommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandParameterValues");

            migrationBuilder.DropTable(
                name: "CommandAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Commands_Name",
                table: "Commands");

            migrationBuilder.DeleteData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Commands",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "CommandFieldNameId",
                table: "Commands",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParsingSettingsId",
                table: "Commands",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CommandParameters",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "CommandParameters",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "CommandId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CommandFieldNameId", "Description", "Name", "ParsingSettingsId" },
                values: new object[] { 1, "Возвращает строку заданной длины начиная начиная с конца исходной строки", "GetTextFromEnd", 2 });

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CommandFieldNameId", "Description", "Name", "ParsingSettingsId" },
                values: new object[] { 2, "Возвращает числа из текстовой строки", "GetNumberFromText", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_CommandFieldNameId",
                table: "Commands",
                column: "CommandFieldNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ParsingSettingsId",
                table: "Commands",
                column: "ParsingSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_AssignmentFieldNames_CommandFieldNameId",
                table: "Commands",
                column: "CommandFieldNameId",
                principalTable: "AssignmentFieldNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_ParsingSettings_ParsingSettingsId",
                table: "Commands",
                column: "ParsingSettingsId",
                principalTable: "ParsingSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_AssignmentFieldNames_CommandFieldNameId",
                table: "Commands");

            migrationBuilder.DropForeignKey(
                name: "FK_Commands_ParsingSettings_ParsingSettingsId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_CommandFieldNameId",
                table: "Commands");

            migrationBuilder.DropIndex(
                name: "IX_Commands_ParsingSettingsId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "CommandFieldNameId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "ParsingSettingsId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CommandParameters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Commands",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CommandParameters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "CommandAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentFieldNameId = table.Column<int>(type: "int", nullable: false),
                    CommandId = table.Column<int>(type: "int", nullable: false),
                    ParsingSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandAssignments_AssignmentFieldNames_AssignmentFieldNameId",
                        column: x => x.AssignmentFieldNameId,
                        principalTable: "AssignmentFieldNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandAssignments_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandParameterValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandAssignmentId = table.Column<int>(type: "int", nullable: false),
                    CommandParameterId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandParameterValues_CommandAssignments_CommandAssignmentId",
                        column: x => x.CommandAssignmentId,
                        principalTable: "CommandAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandParameterValues_CommandParameters_CommandParameterId",
                        column: x => x.CommandParameterId,
                        principalTable: "CommandParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CommandAssignments",
                columns: new[] { "Id", "AssignmentFieldNameId", "CommandId", "ParsingSettingsId" },
                values: new object[,]
                {
                    { 1, 1, 2, 2 },
                    { 2, 2, 1, 2 }
                });

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "CommandId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Возвращает числа из текстовой строки", "GetNumberFromText" });

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Возвращает строку заданной длины начиная начиная с конца исходной строки", "GetTextFromEnd" });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Находит строку и заменяет новой", "ReplaceSubString" });

            migrationBuilder.InsertData(
                table: "CommandParameterValues",
                columns: new[] { "Id", "CommandAssignmentId", "CommandParameterId", "Value" },
                values: new object[] { 1, 1, 1, "3" });

            migrationBuilder.InsertData(
                table: "CommandParameters",
                columns: new[] { "Id", "CommandId", "Description", "Name" },
                values: new object[] { 3, 3, "Новая строка", "NewString" });

            migrationBuilder.InsertData(
                table: "CommandParameters",
                columns: new[] { "Id", "CommandId", "Description", "Name" },
                values: new object[] { 2, 3, "Исходная строка", "OldString" });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_Name",
                table: "Commands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommandAssignments_AssignmentFieldNameId",
                table: "CommandAssignments",
                column: "AssignmentFieldNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandAssignments_CommandId",
                table: "CommandAssignments",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandAssignments_ParsingSettingsId",
                table: "CommandAssignments",
                column: "ParsingSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandParameterValues_CommandAssignmentId",
                table: "CommandParameterValues",
                column: "CommandAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandParameterValues_CommandParameterId",
                table: "CommandParameterValues",
                column: "CommandParameterId");
        }
    }
}
