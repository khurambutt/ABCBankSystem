using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCBankSystem.Data.Migrations
{
    public partial class updateToandFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FromAccount",
                table: "Transaction",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ToAccount",
                table: "Transaction",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "Transaction");
        }
    }
}
