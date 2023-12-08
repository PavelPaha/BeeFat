using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProduct_Foods_FoodId",
                table: "FoodProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodProduct_Tracks_TrackId",
                table: "FoodProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodProduct",
                table: "FoodProduct");

            migrationBuilder.RenameTable(
                name: "FoodProduct",
                newName: "FoodProducts");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProduct_TrackId",
                table: "FoodProducts",
                newName: "IX_FoodProducts_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProduct_FoodId",
                table: "FoodProducts",
                newName: "IX_FoodProducts_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodProducts",
                table: "FoodProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Foods_FoodId",
                table: "FoodProducts",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Tracks_TrackId",
                table: "FoodProducts",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Foods_FoodId",
                table: "FoodProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Tracks_TrackId",
                table: "FoodProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodProducts",
                table: "FoodProducts");

            migrationBuilder.RenameTable(
                name: "FoodProducts",
                newName: "FoodProduct");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProducts_TrackId",
                table: "FoodProduct",
                newName: "IX_FoodProduct_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProduct",
                newName: "IX_FoodProduct_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodProduct",
                table: "FoodProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProduct_Foods_FoodId",
                table: "FoodProduct",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProduct_Tracks_TrackId",
                table: "FoodProduct",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
