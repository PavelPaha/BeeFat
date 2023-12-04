using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodProductClassStructureChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Foods_FoodId",
                table: "FoodProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodProducts",
                table: "FoodProducts");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "FoodProducts");

            migrationBuilder.RenameTable(
                name: "FoodProducts",
                newName: "FoodProduct");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProduct",
                newName: "IX_FoodProduct_FoodId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "FoodProduct",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Grams",
                table: "FoodProduct",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FoodProduct",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Piece",
                table: "FoodProduct",
                type: "integer",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProduct_Foods_FoodId",
                table: "FoodProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodProduct",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "Grams",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FoodProduct");

            migrationBuilder.DropColumn(
                name: "Piece",
                table: "FoodProduct");

            migrationBuilder.RenameTable(
                name: "FoodProduct",
                newName: "FoodProducts");

            migrationBuilder.RenameIndex(
                name: "IX_FoodProduct_FoodId",
                table: "FoodProducts",
                newName: "IX_FoodProducts_FoodId");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "FoodProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
