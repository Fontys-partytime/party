using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partytime.Party.Service.Migrations
{
    /// <inheritdoc />
    public partial class RelationalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "party",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    starts = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ends = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    location = table.Column<string>(type: "text", nullable: true),
                    budget = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_party", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "joined",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    partyid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true),
                    accepted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_joined", x => x.id);
                    table.ForeignKey(
                        name: "fk_joined_party_partyid",
                        column: x => x.partyid,
                        principalTable: "party",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_joined_partyid",
                table: "joined",
                column: "partyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "joined");

            migrationBuilder.DropTable(
                name: "party");
        }
    }
}
