using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortionSize",
                table: "FoodProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightCalories",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TrackId",
                table: "BeeFatUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BeeFatUsers_TrackId",
                table: "BeeFatUsers",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeeFatUsers_Tracks_TrackId",
                table: "BeeFatUsers",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeeFatUsers_Tracks_TrackId",
                table: "BeeFatUsers");

            migrationBuilder.DropIndex(
                name: "IX_BeeFatUsers_TrackId",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "PortionSize",
                table: "FoodProducts");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "RightCalories",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "BeeFatUsers");
        }
    }
}
