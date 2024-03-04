using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGInventoryManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockSnapshot_Products_ProductId",
                table: "StockSnapshot");

            migrationBuilder.DropForeignKey(
                name: "FK_StockSnapshot_Stocks_StockId",
                table: "StockSnapshot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockSnapshot",
                table: "StockSnapshot");

            migrationBuilder.RenameTable(
                name: "StockSnapshot",
                newName: "StockSnapshots");

            migrationBuilder.RenameIndex(
                name: "IX_StockSnapshot_StockId",
                table: "StockSnapshots",
                newName: "IX_StockSnapshots_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockSnapshot_ProductId",
                table: "StockSnapshots",
                newName: "IX_StockSnapshots_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockSnapshots",
                table: "StockSnapshots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSnapshots_Products_ProductId",
                table: "StockSnapshots",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSnapshots_Stocks_StockId",
                table: "StockSnapshots",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockSnapshots_Products_ProductId",
                table: "StockSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_StockSnapshots_Stocks_StockId",
                table: "StockSnapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockSnapshots",
                table: "StockSnapshots");

            migrationBuilder.RenameTable(
                name: "StockSnapshots",
                newName: "StockSnapshot");

            migrationBuilder.RenameIndex(
                name: "IX_StockSnapshots_StockId",
                table: "StockSnapshot",
                newName: "IX_StockSnapshot_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockSnapshots_ProductId",
                table: "StockSnapshot",
                newName: "IX_StockSnapshot_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockSnapshot",
                table: "StockSnapshot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSnapshot_Products_ProductId",
                table: "StockSnapshot",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSnapshot_Stocks_StockId",
                table: "StockSnapshot",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
