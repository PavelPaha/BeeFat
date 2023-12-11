using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BeeFatUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Journals",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "BeeFatUsers",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Journals",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "BeeFatUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "BeeFatUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "BeeFatUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "BeeFatUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "BeeFatUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "BeeFatUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BeeFatUsers",
                type: "text",
                nullable: true);
        }
    }
}
