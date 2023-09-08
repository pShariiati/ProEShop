using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_09_08_1834 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "ReturnProducts",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProducts_TrackingNumber",
                table: "ReturnProducts",
                column: "TrackingNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReturnProducts_TrackingNumber",
                table: "ReturnProducts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReturnProducts");
        }
    }
}
