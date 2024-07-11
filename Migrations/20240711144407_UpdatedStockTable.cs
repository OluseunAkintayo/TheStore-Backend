using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stocks_StockId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Shop",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "Warehouse",
                table: "Stocks",
                newName: "Quantity");

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Stocks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StockLevelStockId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockId",
                table: "Products",
                column: "StockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockLevelStockId",
                table: "Products",
                column: "StockLevelStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stocks_StockLevelStockId",
                table: "Products",
                column: "StockLevelStockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stocks_StockLevelStockId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockLevelStockId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "StockLevelStockId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Stocks",
                newName: "Warehouse");

            migrationBuilder.AddColumn<int>(
                name: "Shop",
                table: "Stocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockId",
                table: "Products",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stocks_StockId",
                table: "Products",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }
    }
}
