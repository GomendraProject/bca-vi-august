using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bca_vi_august.Migrations
{
    /// <inheritdoc />
    public partial class add_category_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "inv_unit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_inv_unit_CategoryId",
                table: "inv_unit",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_inv_unit_inv_category_CategoryId",
                table: "inv_unit",
                column: "CategoryId",
                principalTable: "inv_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inv_unit_inv_category_CategoryId",
                table: "inv_unit");

            migrationBuilder.DropIndex(
                name: "IX_inv_unit_CategoryId",
                table: "inv_unit");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "inv_unit");
        }
    }
}
