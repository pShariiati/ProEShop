using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_04_26_1336 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SellerId",
                table: "Variants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_SellerId",
                table: "Variants",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Sellers_SellerId",
                table: "Variants",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Sellers_SellerId",
                table: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_Variants_SellerId",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Variants");
        }
    }
}
