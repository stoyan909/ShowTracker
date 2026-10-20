using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShowTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddetGenresToExistingShowInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShowGenres",
                columns: new[] { "GenreId", "ShowId" },
                values: new object[,]
                {
                    { 3, new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") },
                    { 4, new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") },
                    { 1, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") },
                    { 5, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") },
                    { 7, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") },
                    { 2, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") },
                    { 3, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") },
                    { 6, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 3, new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 4, new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 1, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 5, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 7, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 2, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 3, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") });

            migrationBuilder.DeleteData(
                table: "ShowGenres",
                keyColumns: new[] { "GenreId", "ShowId" },
                keyValues: new object[] { 6, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") });
        }
    }
}
