using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_05_15_1728 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Sellers_SellerId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_SellerId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Sellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Sellers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_SellerId",
                table: "Sellers",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Sellers_SellerId",
                table: "Sellers",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }
    }
}
