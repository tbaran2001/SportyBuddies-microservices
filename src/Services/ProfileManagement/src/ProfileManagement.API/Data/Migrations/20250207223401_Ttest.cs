using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileManagement.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ttest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Profiles",
                newName: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Profiles",
                newName: "DateOfBirth");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "Profiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
