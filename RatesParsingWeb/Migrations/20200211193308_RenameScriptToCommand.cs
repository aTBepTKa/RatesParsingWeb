using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class RenameScriptToCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScriptParameters");

            migrationBuilder.DropTable(
                name: "TextCodeScriptParameters");

            migrationBuilder.DropTable(
                name: "UnitScriptParameters");

            migrationBuilder.DropTable(
                name: "TextCodeScriptAssignments");

            migrationBuilder.DropTable(
                name: "UnitScriptAssignments");

            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    CommandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandParameters_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextCodeCommandAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandId = table.Column<int>(nullable: false),
                    ParsingSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCodeCommandAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCodeCommandAssignments_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextCodeCommandAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitCommandAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandId = table.Column<int>(nullable: false),
                    ParsingSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitCommandAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitCommandAssignments_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitCommandAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextCodeCommandParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 50, nullable: false),
                    TextCodeCommandAssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCodeCommandParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCodeCommandParameters_TextCodeCommandAssignments_TextCodeCommandAssignmentId",
                        column: x => x.TextCodeCommandAssignmentId,
                        principalTable: "TextCodeCommandAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitCommandParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 50, nullable: false),
                    UnitCommandAssignmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitCommandParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitCommandParameters_UnitCommandAssignments_UnitCommandAssignmentId",
                        column: x => x.UnitCommandAssignmentId,
                        principalTable: "UnitCommandAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 1, "Возвращает числа из текстовой строки", "Получить числа из текста", "GetNumberFromText" });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 2, "Возвращает строку заданной длины начиная начиная с конца исходной строки", "Получить текст с конца строки", "GetTextFromEnd" });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 3, "Находит строку и заменяет новой", "Заменить строку", "ReplaceSubString" });

            migrationBuilder.InsertData(
                table: "CommandParameters",
                columns: new[] { "Id", "CommandId", "FullName", "Name" },
                values: new object[] { 1, 2, "Длина строки", "Length" });

            migrationBuilder.InsertData(
                table: "CommandParameters",
                columns: new[] { "Id", "CommandId", "FullName", "Name" },
                values: new object[] { 2, 3, "Исходная строка", "OldString" });

            migrationBuilder.InsertData(
                table: "CommandParameters",
                columns: new[] { "Id", "CommandId", "FullName", "Name" },
                values: new object[] { 3, 3, "Новая строка", "NewString" });

            migrationBuilder.CreateIndex(
                name: "IX_CommandParameters_CommandId",
                table: "CommandParameters",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_Name",
                table: "Commands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeCommandAssignments_CommandId",
                table: "TextCodeCommandAssignments",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeCommandAssignments_ParsingSettingsId",
                table: "TextCodeCommandAssignments",
                column: "ParsingSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TextCodeCommandParameters_TextCodeCommandAssignmentId",
                table: "TextCodeCommandParameters",
                column: "TextCodeCommandAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitCommandAssignments_CommandId",
                table: "UnitCommandAssignments",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitCommandAssignments_ParsingSettingsId",
                table: "UnitCommandAssignments",
                column: "ParsingSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitCommandParameters_UnitCommandAssignmentId",
                table: "UnitCommandParameters",
                column: "UnitCommandAssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandParameters");

            migrationBuilder.DropTable(
                name: "TextCodeCommandParameters");

            migrationBuilder.DropTable(
                name: "UnitCommandParameters");

            migrationBuilder.DropTable(
                name: "TextCodeCommandAssignments");

            migrationBuilder.DropTable(
                name: "UnitCommandAssignments");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScriptParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ScriptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScriptParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScriptParameters_Scripts_ScriptId",
                        column: x => x.ScriptId,
                        principalTable: "Scripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextCodeScriptAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParsingSettingsId = table.Column<int>(type: "int", nullable: false),
                    ScriptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextCodeScriptAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextCodeScriptAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParsingSettingsId = table.Column<int>(type: "int", nullable: false),
                    ScriptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitScriptAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitScriptAssignments_ParsingSettings_ParsingSettingsId",
                        column: x => x.ParsingSettingsId,
                        principalTable: "ParsingSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextCodeScriptAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitScriptAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                table: "Scripts",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 1, "Возвращает числа из текстовой строки", "Получить числа из текста", "GetNumberFromText" });

            migrationBuilder.InsertData(
                table: "Scripts",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 2, "Возвращает строку заданной длины начиная начиная с конца исходной строки", "Получить текст с конца строки", "GetTextFromEnd" });

            migrationBuilder.InsertData(
                table: "Scripts",
                columns: new[] { "Id", "Description", "FullName", "Name" },
                values: new object[] { 3, "Находит строку и заменяет новой", "Заменить строку", "ReplaceSubString" });

            migrationBuilder.InsertData(
                table: "ScriptParameters",
                columns: new[] { "Id", "FullName", "Name", "ScriptId" },
                values: new object[] { 1, "Длина строки", "Length", 2 });

            migrationBuilder.InsertData(
                table: "ScriptParameters",
                columns: new[] { "Id", "FullName", "Name", "ScriptId" },
                values: new object[] { 2, "Исходная строка", "OldString", 3 });

            migrationBuilder.InsertData(
                table: "ScriptParameters",
                columns: new[] { "Id", "FullName", "Name", "ScriptId" },
                values: new object[] { 3, "Новая строка", "NewString", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_ScriptParameters_ScriptId",
                table: "ScriptParameters",
                column: "ScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_Name",
                table: "Scripts",
                column: "Name",
                unique: true);

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
    }
}
