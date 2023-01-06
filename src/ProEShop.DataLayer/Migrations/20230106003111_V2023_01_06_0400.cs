using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_01_06_0400 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsQuestionsAndAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsUnknown = table.Column<bool>(type: "bit", nullable: false),
                    IsBuyer = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    SellerId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProductsQuestionsAndAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAndAnswers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAndAnswers_ProductsQuestionsAndAnswers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductsQuestionsAndAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAndAnswers_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAndAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsQuestionsAnswersScores",
                columns: table => new
                {
                    AnswerId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsQuestionsAnswersScores", x => new { x.UserId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAnswersScores_ProductsQuestionsAndAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "ProductsQuestionsAndAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsQuestionsAnswersScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuestionsAndAnswers_ParentId",
                table: "ProductsQuestionsAndAnswers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuestionsAndAnswers_ProductId",
                table: "ProductsQuestionsAndAnswers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuestionsAndAnswers_SellerId",
                table: "ProductsQuestionsAndAnswers",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuestionsAndAnswers_UserId",
                table: "ProductsQuestionsAndAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuestionsAnswersScores_AnswerId",
                table: "ProductsQuestionsAnswersScores",
                column: "AnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropTable(
                name: "ProductsQuestionsAndAnswers");
        }
    }
}
