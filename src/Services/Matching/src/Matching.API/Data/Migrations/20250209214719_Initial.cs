using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matching.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OppositeMatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchedProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Swipe = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Unknown"),
                    SwipedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
