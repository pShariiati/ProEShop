using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEShop.DataLayer.Migrations
{
    public partial class V2021_11_05_1747 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "UserTokens",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "UserTokens",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "UserTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "UserTokens",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "UserTokens",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "UserTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "Users",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "Users",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "UserRoles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "UserRoles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UserRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "UserRoles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "UserRoles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "UserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "UserLogins",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "UserLogins",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "UserLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UserLogins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "UserLogins",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "UserLogins",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "UserLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "UserLogins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "UserClaims",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "UserClaims",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "UserClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "UserClaims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "UserClaims",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "UserClaims",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "UserClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "UserClaims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "Roles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "Roles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "Roles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "Roles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "RoleClaims",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "RoleClaims",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RoleClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "RoleClaims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "RoleClaims",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "RoleClaims",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "RoleClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "RoleClaims",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "RoleClaims");
        }
    }
}
