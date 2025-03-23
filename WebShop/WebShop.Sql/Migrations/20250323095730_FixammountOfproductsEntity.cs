using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Sql.Migrations
{
    /// <inheritdoc />
    public partial class FixammountOfproductsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProductItem");

            migrationBuilder.CreateTable(
                name: "AmountOfProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsAmmount = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountOfProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmountOfProducts_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AmountOfProducts_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AmountOfProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmountOfProducts_CartId",
                table: "AmountOfProducts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AmountOfProducts_OrderId",
                table: "AmountOfProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AmountOfProducts_ProductId",
                table: "AmountOfProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmountOfProducts");

            migrationBuilder.CreateTable(
                name: "CartProductItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsAmmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProductItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartProductItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItem_CartId",
                table: "CartProductItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItem_OrderId",
                table: "CartProductItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductItem_ProductId",
                table: "CartProductItem",
                column: "ProductId");
        }
    }
}
