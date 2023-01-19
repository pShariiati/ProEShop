using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2023_01_19_0855 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "ProductsQuestionsAnswersScores",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "ProductsQuestionsAnswersScores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "ProductsQuestionsAnswersScores",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "ProductsQuestionsAnswersScores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "ProductsQuestionsAnswersScores",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "ProductsQuestionsAnswersScores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByUserId",
                table: "ProductsQuestionsAnswersScores",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "ProductsQuestionsAnswersScores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserListShortLinks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    DisplayLink = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_UserListShortLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserListShortLinkId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_UserLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLists_UserListShortLinks_UserListShortLinkId",
                        column: x => x.UserListShortLinkId,
                        principalTable: "UserListShortLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserListsProducts",
                columns: table => new
                {
                    UserListId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_UserListsProducts", x => new { x.UserListId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_UserListsProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserListsProducts_UserLists_UserListId",
                        column: x => x.UserListId,
                        principalTable: "UserLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_Title_UserId",
                table: "UserLists",
                columns: new[] { "Title", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_UserId",
                table: "UserLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_UserListShortLinkId",
                table: "UserLists",
                column: "UserListShortLinkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserListShortLinks_Link",
                table: "UserListShortLinks",
                column: "Link",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserListsProducts_ProductId",
                table: "UserListsProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserListsProducts");

            migrationBuilder.DropTable(
                name: "UserLists");

            migrationBuilder.DropTable(
                name: "UserListShortLinks");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "ProductsQuestionsAnswersScores");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "ProductsQuestionsAnswersScores");
        }
    }
}
