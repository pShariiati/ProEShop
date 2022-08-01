using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_08_02_0041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductShortLinks_ProductShortLinkId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductShortLinkId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "ProductShortLinkId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductShortLinkId",
                table: "Products",
                column: "ProductShortLinkId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductShortLinks_ProductShortLinkId",
                table: "Products",
                column: "ProductShortLinkId",
                principalTable: "ProductShortLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductShortLinks_ProductShortLinkId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductShortLinkId",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "ProductShortLinkId",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductShortLinkId",
                table: "Products",
                column: "ProductShortLinkId",
                unique: true,
                filter: "[ProductShortLinkId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductShortLinks_ProductShortLinkId",
                table: "Products",
                column: "ProductShortLinkId",
                principalTable: "ProductShortLinks",
                principalColumn: "Id");
        }
    }
}
