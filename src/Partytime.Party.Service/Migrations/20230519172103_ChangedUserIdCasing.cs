using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partytime.Party.Service.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserIdCasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "party",
                newName: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userid",
                table: "party",
                newName: "user_id");
        }
    }
}
