using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_03_18_0435 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Brands_TitleEn",
                table: "Brands",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_TitleFa",
                table: "Brands",
                column: "TitleFa",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Brands_TitleEn",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_TitleFa",
                table: "Brands");
        }
    }
}
