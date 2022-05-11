using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_05_11_0828 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_FeatureId",
                table: "ProductFeatures");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_FeatureId_ProductId",
                table: "ProductFeatures",
                columns: new[] { "FeatureId", "ProductId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_FeatureId_ProductId",
                table: "ProductFeatures");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_FeatureId",
                table: "ProductFeatures",
                column: "FeatureId");
        }
    }
}
