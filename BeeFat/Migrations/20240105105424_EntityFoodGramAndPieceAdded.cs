using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityFoodGramAndPieceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightCalories",
                table: "BeeFatUsers");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Foods",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Foods");

            migrationBuilder.AddColumn<int>(
                name: "RightCalories",
                table: "BeeFatUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
