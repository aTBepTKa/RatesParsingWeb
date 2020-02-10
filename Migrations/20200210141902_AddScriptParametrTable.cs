using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class AddScriptParametrTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParamsNum",
                table: "Scripts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Scripts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Scripts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScriptParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    FullName = table.Column<string>(maxLength: 50, nullable: true),
                    ScriptId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ScriptParameters_ScriptId",
                table: "ScriptParameters",
                column: "ScriptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScriptParameters");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Scripts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Scripts");

            migrationBuilder.AddColumn<int>(
                name: "ParamsNum",
                table: "Scripts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
