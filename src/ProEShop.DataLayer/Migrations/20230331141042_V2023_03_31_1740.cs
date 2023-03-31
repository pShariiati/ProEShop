using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_03_31_1740 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GiftCardId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GiftCardPrice",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    MinimumPriceOfCart = table.Column<int>(type: "int", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCards_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftCards_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftCards_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GiftCardId",
                table: "Orders",
                column: "GiftCardId",
                unique: true,
                filter: "[GiftCardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_BrandId",
                table: "GiftCards",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_CategoryId",
                table: "GiftCards",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_Code",
                table: "GiftCards",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCards_SellerId",
                table: "GiftCards",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_GiftCards_GiftCardId",
                table: "Orders",
                column: "GiftCardId",
                principalTable: "GiftCards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_GiftCards_GiftCardId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "GiftCards");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GiftCardId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GiftCardId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GiftCardPrice",
                table: "Orders");
        }
    }
}
