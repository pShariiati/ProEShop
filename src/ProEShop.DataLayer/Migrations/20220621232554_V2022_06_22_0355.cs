using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_06_22_0355 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsignmentItems_ConsignmentId",
                table: "ConsignmentItems");

            migrationBuilder.CreateIndex(
                name: "IX_ConsignmentItems_ConsignmentId_ProductVariantId",
                table: "ConsignmentItems",
                columns: new[] { "ConsignmentId", "ProductVariantId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsignmentItems_ConsignmentId_ProductVariantId",
                table: "ConsignmentItems");

            migrationBuilder.CreateIndex(
                name: "IX_ConsignmentItems_ConsignmentId",
                table: "ConsignmentItems",
                column: "ConsignmentId");
        }
    }
}
