using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_05_18_1346 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MainCategoryId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainCategoryId",
                table: "Products",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_MainCategoryId",
                table: "Products",
                column: "MainCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_MainCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Products");
        }
    }
}
