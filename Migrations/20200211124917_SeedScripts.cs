using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class SeedScripts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScriptParameters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScriptParameters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ScriptParameters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scripts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Scripts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scripts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
