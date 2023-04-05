using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_04_05_2109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GiftCardPrice",
                table: "Orders",
                newName: "GiftCardCodePrice");

            migrationBuilder.AddColumn<long>(
                name: "ReservedGiftCardId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservedGiftCardId",
                table: "Orders",
                column: "ReservedGiftCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GiftCards_ReservedGiftCardId",
                table: "Orders",
                column: "ReservedGiftCardId",
                principalTable: "GiftCards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GiftCards_ReservedGiftCardId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReservedGiftCardId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReservedGiftCardId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "GiftCardCodePrice",
                table: "Orders",
                newName: "GiftCardPrice");
        }
    }
}
