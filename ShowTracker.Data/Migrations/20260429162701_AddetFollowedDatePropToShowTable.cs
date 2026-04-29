using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddetFollowedDatePropToShowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Shows");

            migrationBuilder.AddColumn<DateTime>(
                name: "FollowedDate",
                table: "UsersShows",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowedDate",
                table: "UsersShows");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Shows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Shows",
                keyColumn: "Id",
                keyValue: new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"),
                columns: new string[0],
                values: new object[0]);
        }
    }
}
