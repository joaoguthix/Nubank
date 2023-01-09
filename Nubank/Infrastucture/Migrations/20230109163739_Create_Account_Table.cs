using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class Create_Account_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MSN_IMAGE",
                table: "TB_DEBIT_CARD");

            migrationBuilder.AddColumn<int>(
                name: "CurrentAccountId",
                table: "TB_DEBIT_CARD",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CURRENT_ACCOUNT",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACCOUNT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BALANCE = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENT_ACCOUNT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CURRENT_ACCOUNT_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_DEBIT_CARD_CurrentAccountId",
                table: "TB_DEBIT_CARD",
                column: "CurrentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CURRENT_ACCOUNT_UserId",
                table: "CURRENT_ACCOUNT",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_DEBIT_CARD_CURRENT_ACCOUNT_CurrentAccountId",
                table: "TB_DEBIT_CARD",
                column: "CurrentAccountId",
                principalTable: "CURRENT_ACCOUNT",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_DEBIT_CARD_CURRENT_ACCOUNT_CurrentAccountId",
                table: "TB_DEBIT_CARD");

            migrationBuilder.DropTable(
                name: "CURRENT_ACCOUNT");

            migrationBuilder.DropIndex(
                name: "IX_TB_DEBIT_CARD_CurrentAccountId",
                table: "TB_DEBIT_CARD");

            migrationBuilder.DropColumn(
                name: "CurrentAccountId",
                table: "TB_DEBIT_CARD");

            migrationBuilder.AddColumn<int>(
                name: "MSN_IMAGE",
                table: "TB_DEBIT_CARD",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
