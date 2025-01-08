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
                    { new Guid("2680f1f5-cadb-4011-8a44-a75e06c49ae9"), new DateTime(2025, 1, 8, 14, 9, 33, 290, DateTimeKind.Utc).AddTicks(1962), new Guid("087e44eb-9044-4ebb-b2e1-0e67d12a7395"), new Guid("d6410e3d-3f8e-4216-a982-daca6402309b"), new Guid("13ba1cf4-831b-4d88-9545-471b352a0744") },
                    { new Guid("3f6d154b-832d-4666-862c-97b5e47ef18c"), new DateTime(2025, 1, 8, 14, 9, 33, 290, DateTimeKind.Utc).AddTicks(1985), new Guid("087e44eb-9044-4ebb-b2e1-0e67d12a7395"), new Guid("f980fdcf-99ad-47e5-8b71-e515c7990fa1"), new Guid("0efcc60c-59b2-4678-ac00-46d24cf870ea") },
                    { new Guid("d6410e3d-3f8e-4216-a982-daca6402309b"), new DateTime(2025, 1, 8, 14, 9, 33, 290, DateTimeKind.Utc).AddTicks(1962), new Guid("13ba1cf4-831b-4d88-9545-471b352a0744"), new Guid("2680f1f5-cadb-4011-8a44-a75e06c49ae9"), new Guid("087e44eb-9044-4ebb-b2e1-0e67d12a7395") },
                    { new Guid("f980fdcf-99ad-47e5-8b71-e515c7990fa1"), new DateTime(2025, 1, 8, 14, 9, 33, 290, DateTimeKind.Utc).AddTicks(1985), new Guid("0efcc60c-59b2-4678-ac00-46d24cf870ea"), new Guid("3f6d154b-832d-4666-862c-97b5e47ef18c"), new Guid("087e44eb-9044-4ebb-b2e1-0e67d12a7395") }
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
