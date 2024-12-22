using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Buddies.Grpc.Migrations
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OppositeBuddyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MatchedProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    { new Guid("05f27813-c15a-4263-bb7e-a39c97844ade"), new DateTime(2024, 12, 21, 23, 27, 47, 364, DateTimeKind.Utc).AddTicks(3325), new Guid("7d2e9d97-9813-4431-8c4b-bfb7168a39de"), new Guid("144e8dac-820b-44ba-b2c5-de68b0615e16"), new Guid("0d76b338-8575-43d9-afb0-b5af4c40a3a8") },
                    { new Guid("144e8dac-820b-44ba-b2c5-de68b0615e16"), new DateTime(2024, 12, 21, 23, 27, 47, 364, DateTimeKind.Utc).AddTicks(3325), new Guid("0d76b338-8575-43d9-afb0-b5af4c40a3a8"), new Guid("05f27813-c15a-4263-bb7e-a39c97844ade"), new Guid("7d2e9d97-9813-4431-8c4b-bfb7168a39de") },
                    { new Guid("7427e169-da90-461f-839e-6f6f1e496c5f"), new DateTime(2024, 12, 21, 23, 27, 47, 364, DateTimeKind.Utc).AddTicks(3340), new Guid("0d76b338-8575-43d9-afb0-b5af4c40a3a8"), new Guid("bec9834b-dcdb-423d-930c-f2516d1737bc"), new Guid("f85e8537-8b27-4259-80ae-14716f8d5a3a") },
                    { new Guid("bec9834b-dcdb-423d-930c-f2516d1737bc"), new DateTime(2024, 12, 21, 23, 27, 47, 364, DateTimeKind.Utc).AddTicks(3340), new Guid("f85e8537-8b27-4259-80ae-14716f8d5a3a"), new Guid("7427e169-da90-461f-839e-6f6f1e496c5f"), new Guid("0d76b338-8575-43d9-afb0-b5af4c40a3a8") }
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
