using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityJournalUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Journals_UserId",
                table: "Journals");

            migrationBuilder.AddColumn<Guid>(
                name: "JournalId",
                table: "BeeFatUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Journals_UserId",
                table: "Journals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BeeFatUsers_JournalId",
                table: "BeeFatUsers",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeeFatUsers_Journals_JournalId",
                table: "BeeFatUsers",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeeFatUsers_Journals_JournalId",
                table: "BeeFatUsers");

            migrationBuilder.DropIndex(
                name: "IX_Journals_UserId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_BeeFatUsers_JournalId",
                table: "BeeFatUsers");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "BeeFatUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_UserId",
                table: "Journals",
                column: "UserId",
                unique: true);
        }
    }
}
