using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_08_07_1737 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "ProductVariants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "OffPercentage",
                table: "ProductVariants",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OffPrice",
                table: "ProductVariants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDatTime",
                table: "ProductVariants",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "OffPercentage",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "OffPrice",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "StartDatTime",
                table: "ProductVariants");
        }
    }
}
