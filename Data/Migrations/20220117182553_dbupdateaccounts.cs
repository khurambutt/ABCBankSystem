using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCBankSystem.Data.Migrations
{
    public partial class dbupdateaccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogedInUserName",
                table: "BankAccount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogedInUserName",
                table: "BankAccount");
        }
    }
}
