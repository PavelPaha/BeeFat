using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityJournalUpdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodProducts_Journals_JournalId",
                table: "FoodProducts");

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

            migrationBuilder.CreateIndex(
                name: "IX_FoodProducts_JournalId",
                table: "FoodProducts",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodProducts_Journals_JournalId",
                table: "FoodProducts",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }
    }
}
