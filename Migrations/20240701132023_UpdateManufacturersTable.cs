using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManufacturersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Manufacturers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Manufacturers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Manufacturers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Manufacturers");
        }
    }
}
