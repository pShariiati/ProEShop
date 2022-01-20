using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_01_20_2306 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_ProvincesAndCities_CityId",
                table: "Sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_ProvincesAndCities_ProvinceId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ShabaNumber",
                table: "Sellers");

            migrationBuilder.AlterColumn<string>(
                name: "ShabaNumber",
                table: "Sellers",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProvinceId",
                table: "Sellers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Sellers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Sellers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Sellers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ShabaNumber",
                table: "Sellers",
                column: "ShabaNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_ProvincesAndCities_CityId",
                table: "Sellers",
                column: "CityId",
                principalTable: "ProvincesAndCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_ProvincesAndCities_ProvinceId",
                table: "Sellers",
                column: "ProvinceId",
                principalTable: "ProvincesAndCities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_ProvincesAndCities_CityId",
                table: "Sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_ProvincesAndCities_ProvinceId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ShabaNumber",
                table: "Sellers");

            migrationBuilder.AlterColumn<string>(
                name: "ShabaNumber",
                table: "Sellers",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<long>(
                name: "ProvinceId",
                table: "Sellers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Sellers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Sellers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Sellers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ShabaNumber",
                table: "Sellers",
                column: "ShabaNumber",
                unique: true,
                filter: "[ShabaNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_ProvincesAndCities_CityId",
                table: "Sellers",
                column: "CityId",
                principalTable: "ProvincesAndCities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_ProvincesAndCities_ProvinceId",
                table: "Sellers",
                column: "ProvinceId",
                principalTable: "ProvincesAndCities",
                principalColumn: "Id");
        }
    }
}
