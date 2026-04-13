using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedANamingTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEpisodes_AspNetUsers_UserId",
                table: "UserEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEpisodes_Episodes_EpisodeId",
                table: "UserEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEpisodes",
                table: "UserEpisodes");

            migrationBuilder.RenameTable(
                name: "UserEpisodes",
                newName: "UsersEpisodes");

            migrationBuilder.RenameIndex(
                name: "IX_UserEpisodes_EpisodeId",
                table: "UsersEpisodes",
                newName: "IX_UsersEpisodes_EpisodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEpisodes",
                table: "UsersEpisodes",
                columns: new[] { "UserId", "EpisodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEpisodes_AspNetUsers_UserId",
                table: "UsersEpisodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersEpisodes_Episodes_EpisodeId",
                table: "UsersEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersEpisodes_AspNetUsers_UserId",
                table: "UsersEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersEpisodes_Episodes_EpisodeId",
                table: "UsersEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEpisodes",
                table: "UsersEpisodes");

            migrationBuilder.RenameTable(
                name: "UsersEpisodes",
                newName: "UserEpisodes");

            migrationBuilder.RenameIndex(
                name: "IX_UsersEpisodes_EpisodeId",
                table: "UserEpisodes",
                newName: "IX_UserEpisodes_EpisodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEpisodes",
                table: "UserEpisodes",
                columns: new[] { "UserId", "EpisodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserEpisodes_AspNetUsers_UserId",
                table: "UserEpisodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEpisodes_Episodes_EpisodeId",
                table: "UserEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
