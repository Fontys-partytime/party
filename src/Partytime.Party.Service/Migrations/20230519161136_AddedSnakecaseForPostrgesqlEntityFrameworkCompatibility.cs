using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partytime.Party.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddedSnakecaseForPostrgesqlEntityFrameworkCompatibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_party",
                table: "party");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "party",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Starts",
                table: "party",
                newName: "starts");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "party",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Ends",
                table: "party",
                newName: "ends");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "party",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "party",
                newName: "budget");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "party",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "party",
                newName: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_party",
                table: "party",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_party",
                table: "party");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "party",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "starts",
                table: "party",
                newName: "Starts");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "party",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "ends",
                table: "party",
                newName: "Ends");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "party",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "budget",
                table: "party",
                newName: "Budget");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "party",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "party",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_party",
                table: "party",
                column: "Id");
        }
    }
}
