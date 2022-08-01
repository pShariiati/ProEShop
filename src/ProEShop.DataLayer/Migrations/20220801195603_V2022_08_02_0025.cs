using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_08_02_0025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductShortLinkId",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductShortLinks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByBrowserName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShortLinks", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductShortLinks_ProductShortLinkId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductShortLinks");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductShortLinkId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductShortLinkId",
                table: "Products");
        }
    }
}
