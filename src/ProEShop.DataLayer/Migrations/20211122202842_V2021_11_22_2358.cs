using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2021_11_22_2358 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParnetId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowInMenus",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(130)",
                maxLength: 130,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Prouducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByBrowserName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prouducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prouducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParnetId",
                table: "Categories",
                column: "ParnetId");

            migrationBuilder.CreateIndex(
                name: "IX_Prouducts_CategoryId",
                table: "Prouducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParnetId",
                table: "Categories",
                column: "ParnetId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParnetId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Prouducts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParnetId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParnetId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ShowInMenus",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
