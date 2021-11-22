using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2021_11_22_2359 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParnetId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParnetId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParnetId",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "ParnetId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParnetId",
                table: "Categories",
                column: "ParnetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParnetId",
                table: "Categories",
                column: "ParnetId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
