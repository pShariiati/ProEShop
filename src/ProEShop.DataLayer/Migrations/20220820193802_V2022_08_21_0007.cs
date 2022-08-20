using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2022_08_21_0007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_SellerId_ProductId_VariantId",
                table: "ProductVariants");

            migrationBuilder.AlterColumn<long>(
                name: "VariantId",
                table: "ProductVariants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SellerId_ProductId_VariantId",
                table: "ProductVariants",
                columns: new[] { "SellerId", "ProductId", "VariantId" },
                unique: true,
                filter: "[VariantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_SellerId_ProductId_VariantId",
                table: "ProductVariants");

            migrationBuilder.AlterColumn<long>(
                name: "VariantId",
                table: "ProductVariants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SellerId_ProductId_VariantId",
                table: "ProductVariants",
                columns: new[] { "SellerId", "ProductId", "VariantId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Variants_VariantId",
                table: "ProductVariants",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
