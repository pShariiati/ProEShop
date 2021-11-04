using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2021_11_04_1412 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Categories");
        }
    }
}
