using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Buddies.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buddies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OppositeBuddyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchedProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buddies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Buddies",
                columns: new[] { "Id", "CreatedOnUtc", "MatchedProfileId", "OppositeBuddyId", "ProfileId" },
                values: new object[,]
                {
                    { new Guid("8fc5fbad-bdbd-4adc-bf0c-14c0f5d6bb02"), new DateTime(2025, 2, 8, 21, 22, 19, 793, DateTimeKind.Utc).AddTicks(4625), new Guid("6e57134d-939d-407b-979a-bb7403fb43a2"), new Guid("e914ad86-d518-4792-aa83-54d1e732b263"), new Guid("b9f551f2-307d-44cb-8a94-adba9d804cfe") },
                    { new Guid("b6a6486c-cadd-4046-bbc9-66ab6e7e6d1b"), new DateTime(2025, 2, 8, 21, 22, 19, 793, DateTimeKind.Utc).AddTicks(4608), new Guid("6e57134d-939d-407b-979a-bb7403fb43a2"), new Guid("f7097c74-1bcc-4f5b-80d7-949720c2c1b7"), new Guid("45662e8e-71f1-4de0-b69d-0f70ba09bd71") },
                    { new Guid("e914ad86-d518-4792-aa83-54d1e732b263"), new DateTime(2025, 2, 8, 21, 22, 19, 793, DateTimeKind.Utc).AddTicks(4625), new Guid("b9f551f2-307d-44cb-8a94-adba9d804cfe"), new Guid("8fc5fbad-bdbd-4adc-bf0c-14c0f5d6bb02"), new Guid("6e57134d-939d-407b-979a-bb7403fb43a2") },
                    { new Guid("f7097c74-1bcc-4f5b-80d7-949720c2c1b7"), new DateTime(2025, 2, 8, 21, 22, 19, 793, DateTimeKind.Utc).AddTicks(4608), new Guid("45662e8e-71f1-4de0-b69d-0f70ba09bd71"), new Guid("b6a6486c-cadd-4046-bbc9-66ab6e7e6d1b"), new Guid("6e57134d-939d-407b-979a-bb7403fb43a2") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buddies");
        }
    }
}
