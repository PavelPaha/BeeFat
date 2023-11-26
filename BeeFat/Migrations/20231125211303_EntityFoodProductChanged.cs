using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodProductChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodProducts_Id",
                table: "Foods");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Foods_Id",
                table: "FoodProducts",
                column: "Id",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Foods_Id",
                table: "FoodProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodProducts_Id",
                table: "Foods",
                column: "Id",
                principalTable: "FoodProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
