using Microsoft.EntityFrameworkCore.Migrations;

namespace RatesParsingWeb.Migrations
{
    public partial class UpdatePropertiesInCommandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "CommandParameters");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Commands",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CommandParameters",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Длина строки");

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Исходная строка");

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Новая строка");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CommandParameters");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Commands",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "CommandParameters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Длина строки");

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Исходная строка");

            migrationBuilder.UpdateData(
                table: "CommandParameters",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Новая строка");

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Получить числа из текста");

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 2,
                column: "FullName",
                value: "Получить текст с конца строки");

            migrationBuilder.UpdateData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 3,
                column: "FullName",
                value: "Заменить строку");
        }
    }
}
