using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_08_03_0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "ProductShortLinks",
                type: "nvarchar(39)",
                maxLength: 39,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_ProductShortLinks_Link",
                table: "ProductShortLinks",
                column: "Link",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductShortLinks_Link",
                table: "ProductShortLinks");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "ProductShortLinks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(39)",
                oldMaxLength: 39);
        }
    }
}
