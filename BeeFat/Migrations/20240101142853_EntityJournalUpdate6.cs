using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class EntityJournalUpdate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalFoodProducts_Journals_JournalId",
                table: "JournalFoodProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalFoodProducts",
                table: "JournalFoodProducts");

            migrationBuilder.RenameTable(
                name: "JournalFoodProducts",
                newName: "JournalFoods");

            migrationBuilder.RenameIndex(
                name: "IX_JournalFoodProducts_JournalId",
                table: "JournalFoods",
                newName: "IX_JournalFoods_JournalId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "JournalFoods",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Grams",
                table: "JournalFoods",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pieces",
                table: "JournalFoods",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalFoods",
                table: "JournalFoods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalFoods_Journals_JournalId",
                table: "JournalFoods",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalFoods_Journals_JournalId",
                table: "JournalFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalFoods",
                table: "JournalFoods");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "JournalFoods");

            migrationBuilder.DropColumn(
                name: "Grams",
                table: "JournalFoods");

            migrationBuilder.DropColumn(
                name: "Pieces",
                table: "JournalFoods");

            migrationBuilder.RenameTable(
                name: "JournalFoods",
                newName: "JournalFoodProducts");

            migrationBuilder.RenameIndex(
                name: "IX_JournalFoods_JournalId",
                table: "JournalFoodProducts",
                newName: "IX_JournalFoodProducts_JournalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalFoodProducts",
                table: "JournalFoodProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalFoodProducts_Journals_JournalId",
                table: "JournalFoodProducts",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }
    }
}
