using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserAddActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Activity",
                table: "BeeFatUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "BeeFatUsers");
        }
    }
}
