using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_03_12_0148 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "ParcelPostItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelPostItems_OrderId",
                table: "ParcelPostItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParcelPostItems_Orders_OrderId",
                table: "ParcelPostItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParcelPostItems_Orders_OrderId",
                table: "ParcelPostItems");

            migrationBuilder.DropIndex(
                name: "IX_ParcelPostItems_OrderId",
                table: "ParcelPostItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ParcelPostItems");
        }
    }
}
