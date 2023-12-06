using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityTrackAdded1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrackId",
                table: "FoodProduct",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodProduct_TrackId",
                table: "FoodProduct",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProduct_Tracks_TrackId",
                table: "FoodProduct",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProduct_Tracks_TrackId",
                table: "FoodProduct");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_FoodProduct_TrackId",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "FoodProduct");
        }
    }
}
