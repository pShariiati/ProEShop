using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_05_28_1540 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryBrands",
                table: "CategoryBrands");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CategoryBrands",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte>(
                name: "CommissionPercentage",
                table: "CategoryBrands",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CategoryBrands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryBrands",
                table: "CategoryBrands",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBrands_CategoryId_BrandId",
                table: "CategoryBrands",
                columns: new[] { "CategoryId", "BrandId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryBrands",
                table: "CategoryBrands");

            migrationBuilder.DropIndex(
                name: "IX_CategoryBrands_CategoryId_BrandId",
                table: "CategoryBrands");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategoryBrands");

            migrationBuilder.DropColumn(
                name: "CommissionPercentage",
                table: "CategoryBrands");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CategoryBrands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryBrands",
                table: "CategoryBrands",
                columns: new[] { "CategoryId", "BrandId" });
        }
    }
}
