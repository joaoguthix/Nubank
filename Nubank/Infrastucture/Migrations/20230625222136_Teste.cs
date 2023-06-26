using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class Teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ACCOUNT",
                table: "CURRENT_ACCOUNT",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "CURRENT_ACCOUNT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATE_ACCOUNT_DATE",
                table: "CURRENT_ACCOUNT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "CURRENT_ACCOUNT",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "CURRENT_ACCOUNT");

            migrationBuilder.DropColumn(
                name: "CREATE_ACCOUNT_DATE",
                table: "CURRENT_ACCOUNT");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "CURRENT_ACCOUNT");

            migrationBuilder.AlterColumn<string>(
                name: "ACCOUNT",
                table: "CURRENT_ACCOUNT",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
