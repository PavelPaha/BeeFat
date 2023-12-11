using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Journals_JournalId",
                table: "FoodProducts");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_FoodProducts_JournalId",
                table: "FoodProducts");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "FoodProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JournalId",
                table: "FoodProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_BeeFatUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "BeeFatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodProducts_JournalId",
                table: "FoodProducts",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_UserId",
                table: "Journals",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Journals_JournalId",
                table: "FoodProducts",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }
    }
}
