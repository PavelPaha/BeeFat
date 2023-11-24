using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeeFat.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "PersonName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "PersonName_FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonName_LastName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "PersonName_FirstName",
                table: "AspNetUsers",
                newName: "FirstName");
        }
    }
}
