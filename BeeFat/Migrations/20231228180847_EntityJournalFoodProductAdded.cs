using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityJournalFoodProductAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JournalFoodProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    JournalId = table.Column<Guid>(type: "uuid", nullable: true),
                    Macronutrient_Proteins = table.Column<int>(type: "integer", nullable: false),
                    Macronutrient_Fats = table.Column<int>(type: "integer", nullable: false),
                    Macronutrient_Carbohydrates = table.Column<int>(type: "integer", nullable: false),
                    Macronutrient_Calories = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    IsEaten = table.Column<bool>(type: "boolean", nullable: false),
                    PortionSize = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalFoodProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalFoodProducts_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalFoodProducts_JournalId",
                table: "JournalFoodProducts",
                column: "JournalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalFoodProducts");
        }
    }
}
