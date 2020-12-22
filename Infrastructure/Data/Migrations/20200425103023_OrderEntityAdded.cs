using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Migrations
{
    public partial class OrderEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(type:"VARCHAR(128)", maxLength: 128, nullable: true),
                    DeliveryTime = table.Column<DateTime>(type:"DATETIME2",nullable: true),
                    Description = table.Column<string>(type:"VARCHAR(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BuyerEmail = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    OrderDate = table.Column<DateTime>(type:"DATETIME2", nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ShipToAddress_LastName = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ShipToAddress_Street = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ShipToAddress_City = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ShipToAddress_State = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ShipToAddress_Zipcode = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    DeliveryMethodId = table.Column<int>(nullable: true),
                    Subtotal = table.Column<decimal>(type:"MONEY", nullable: false),
                    Status = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    PaymentIntentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    PictureUrl = table.Column<string>(type:"VARCHAR(MAX)", nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    ProductBrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                 
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ItemOrdered_ProductItemId = table.Column<int>(nullable: true),
                    ItemOrdered_ProductName = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ItemOrdered_PictureUrl = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryMethodId",
                table: "Order",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");
        }
    }
}
