using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class RefactorCommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextCodeCommandParameters");

            migrationBuilder.DropTable(
                name: "UnitCommandParameters");

            migrationBuilder.DropTable(
                name: "TextCodeCommandAssignments");

            migrationBuilder.DropTable(
                name: "UnitCommandAssignments");

            migrationBuilder.CreateTable(
                name: "AssignmentFieldNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentFieldNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentFieldNameId = table.Column<int>(nullable: false),
                    CommandId = table.Column<int>(nullable: false),
                    ParsingSettingsId = table.Column<int>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 50, nullable: false),
                    CommandParameterId = table.Column<int>(nullable: false),
                    CommandAssignmentId = table.Column<int>(nullable: false)
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
                table: "AssignmentFieldNames",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "TextCode" });

            migrationBuilder.InsertData(
                table: "AssignmentFieldNames",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Unit" });

            migrationBuilder.InsertData(
                table: "AssignmentFieldNames",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "ExchangeRate" });

            migrationBuilder.InsertData(
                table: "CommandAssignments",
                columns: new[] { "Id", "AssignmentFieldNameId", "CommandId", "ParsingSettingsId" },
                values: new object[] { 1, 1, 2, 2 });

            migrationBuilder.InsertData(
                table: "CommandAssignments",
                columns: new[] { "Id", "AssignmentFieldNameId", "CommandId", "ParsingSettingsId" },
                values: new object[] { 2, 2, 1, 2 });

            migrationBuilder.InsertData(
                table: "CommandParameterValues",
                columns: new[] { "Id", "CommandAssignmentId", "CommandParameterId", "Value" },
                values: new object[] { 1, 1, 1, "3" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandParameterValues");

            migrationBuilder.DropTable(
                name: "CommandAssignments");

            migrationBuilder.DropTable(
                name: "AssignmentFieldNames");

            migrationBuilder.CreateTable(
                name: "TextCodeCommandAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandId = table.Column<int>(type: "int", nullable: false),
                    ParsingSettingsId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandId = table.Column<int>(type: "int", nullable: false),
                    ParsingSettingsId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextCodeCommandAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCommandAssignmentId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                table: "TextCodeCommandAssignments",
                columns: new[] { "Id", "CommandId", "ParsingSettingsId" },
                values: new object[] { 1, 2, 2 });

            migrationBuilder.InsertData(
                table: "UnitCommandAssignments",
                columns: new[] { "Id", "CommandId", "ParsingSettingsId" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "TextCodeCommandParameters",
                columns: new[] { "Id", "TextCodeCommandAssignmentId", "Value" },
                values: new object[] { 1, 1, "3" });

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
    }
}
