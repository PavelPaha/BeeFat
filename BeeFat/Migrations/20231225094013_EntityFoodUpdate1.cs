using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts");

            migrationBuilder.CreateIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts");

            migrationBuilder.CreateIndex(
                name: "IX_FoodProducts_FoodId",
                table: "FoodProducts",
                column: "FoodId",
                unique: true);
        }
    }
}
