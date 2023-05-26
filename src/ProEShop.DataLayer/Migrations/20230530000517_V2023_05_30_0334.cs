using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_05_30_0334 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SaleCount",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VisitCount",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "Products");
        }
    }
}
