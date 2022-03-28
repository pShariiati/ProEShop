using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_03_29_0257 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Brands",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SellerId",
                table: "Brands",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Sellers_SellerId",
                table: "Brands",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Sellers_SellerId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_SellerId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Brands");
        }
    }
}
