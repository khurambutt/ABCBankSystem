using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCBankSystem.Data.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Transaction_TransactionID",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_TransactionID",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "BankAccount");

            migrationBuilder.AddColumn<long>(
                name: "BankAccountNumber",
                table: "Transaction",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionID",
                table: "BankAccount",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_TransactionID",
                table: "BankAccount",
                column: "TransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Transaction_TransactionID",
                table: "BankAccount",
                column: "TransactionID",
                principalTable: "Transaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
