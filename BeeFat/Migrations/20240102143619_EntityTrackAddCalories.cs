using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityTrackAddCalories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaloriesByDay",
                table: "Tracks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesByDay",
                table: "Tracks");
        }
    }
}
