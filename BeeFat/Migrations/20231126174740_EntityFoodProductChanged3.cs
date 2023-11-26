using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodProductChanged3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodProducts_FoodProductId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodProductId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FoodProductId",
                table: "Foods");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "FoodProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts",
                column: "FoodId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Foods_FoodId",
                table: "FoodProducts",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Foods_FoodId",
                table: "FoodProducts");

            migrationBuilder.DropIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "FoodProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<Guid>(
                name: "FoodProductId",
                table: "Foods",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodProductId",
                table: "Foods",
                column: "FoodProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodProducts_FoodProductId",
                table: "Foods",
                column: "FoodProductId",
                principalTable: "FoodProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
