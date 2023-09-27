using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bca_vi_august.Migrations
{
    /// <inheritdoc />
    public partial class add_sales_and_sales_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inv_sales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inv_sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "inv_sales_details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId1 = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inv_sales_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inv_sales_details_inv_sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "inv_sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inv_sales_details_inv_unit_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "inv_unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inv_sales_details_ProductId1",
                table: "inv_sales_details",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_inv_sales_details_SaleId",
                table: "inv_sales_details",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inv_sales_details");

            migrationBuilder.DropTable(
                name: "inv_sales");
        }
    }
}
