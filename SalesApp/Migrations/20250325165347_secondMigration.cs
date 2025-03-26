using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApp.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransaction_Customer_CustomerId",
                table: "SalesTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransaction_Invoice_InvoiceId",
                table: "SalesTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransaction_Products_ProductId",
                table: "SalesTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesTransaction",
                table: "SalesTransaction");

            migrationBuilder.RenameTable(
                name: "SalesTransaction",
                newName: "SalesTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransaction_ProductId",
                table: "SalesTransactions",
                newName: "IX_SalesTransactions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransaction_InvoiceId",
                table: "SalesTransactions",
                newName: "IX_SalesTransactions_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransaction_CustomerId",
                table: "SalesTransactions",
                newName: "IX_SalesTransactions_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesTransactions",
                table: "SalesTransactions",
                column: "SalesTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransactions_Customer_CustomerId",
                table: "SalesTransactions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransactions_Invoice_InvoiceId",
                table: "SalesTransactions",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransactions_Products_ProductId",
                table: "SalesTransactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransactions_Customer_CustomerId",
                table: "SalesTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransactions_Invoice_InvoiceId",
                table: "SalesTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesTransactions_Products_ProductId",
                table: "SalesTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesTransactions",
                table: "SalesTransactions");

            migrationBuilder.RenameTable(
                name: "SalesTransactions",
                newName: "SalesTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransactions_ProductId",
                table: "SalesTransaction",
                newName: "IX_SalesTransaction_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransactions_InvoiceId",
                table: "SalesTransaction",
                newName: "IX_SalesTransaction_InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesTransactions_CustomerId",
                table: "SalesTransaction",
                newName: "IX_SalesTransaction_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesTransaction",
                table: "SalesTransaction",
                column: "SalesTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransaction_Customer_CustomerId",
                table: "SalesTransaction",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransaction_Invoice_InvoiceId",
                table: "SalesTransaction",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTransaction_Products_ProductId",
                table: "SalesTransaction",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
