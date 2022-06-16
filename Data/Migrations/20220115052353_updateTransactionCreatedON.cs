using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCBankSystem.Data.Migrations
{
    public partial class updateTransactionCreatedON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Transaction");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Transaction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "Transaction");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
