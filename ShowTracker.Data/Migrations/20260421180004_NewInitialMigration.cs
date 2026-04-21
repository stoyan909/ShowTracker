using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShowTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersShows",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersShows", x => new { x.UserId, x.ShowId });
                    table.ForeignKey(
                        name: "FK_UsersShows_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersShows_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodeTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWatched = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersEpisodes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersEpisodes", x => new { x.UserId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_UsersEpisodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersEpisodes_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shows",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9"), "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line in the following days, weeks and months.", "Chernobyl" },
                    { new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01"), "A squad of prisoners and their guards are sent to investigate a mysterious island. They get stranded there and must rely on each other to survive the island's mysterious and monstrous residents.", "Hell's Paradise" },
                    { new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"), "Follows Wednesday Addams' years as a student, when she attempts to master her emerging psychic ability, thwart a killing spree, and solve the mystery that embroiled her parents.", "Wednesday" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "SeasonNumber", "ShowId" },
                values: new object[,]
                {
                    { new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808"), 1, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") },
                    { new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde"), 1, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") },
                    { new Guid("87932afe-1b94-46e6-83a3-034b0aa183be"), 2, new Guid("a2b3c4d5-e6f7-8901-2345-6789abcdef01") },
                    { new Guid("9c524886-a88e-49b3-a510-025b76f4cf27"), 2, new Guid("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") },
                    { new Guid("c9d46740-7c4b-4380-8122-adee43c370e8"), 1, new Guid("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") }
                });

            migrationBuilder.InsertData(
                table: "Episodes",
                columns: new[] { "Id", "EpisodeTitle", "ImageUrl", "ReleaseDate", "SeasonId" },
                values: new object[,]
                {
                    { 1, "1:23:45", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/21acd328-6298-4928-bb08-e926d776f63d/a53d81424437914d76ce305e1f1c3634b5736dc9.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320", new DateTime(2019, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 2, "Please Remain Calm", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/7c3b4584-c2b1-482f-bc8c-18c91bf1acf1/87eced8d07e3275e259e8e4325e43abac7cc350d.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320", new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 3, "Open Wide, O Earth", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/e8d738e3-7bd3-40e5-be1a-d6b2c270064c/f9c7ffb5a19c43ade5b4495cc9ac8613a0831315.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 4, "The Happiness of All Mankind", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/7cc1a700-bd77-456c-9d61-aa95cff7d73b/072b882e161aafb797c01c92f2e3a19b66882e1d.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320", new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 5, "Vichnaya Pamyat", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/8d42e46a-0b6d-4acd-85f8-d980ec41769c/0df0fdbb626c582ac918d64a5fb0234a890448dd.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320", new DateTime(2019, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 6, "Wednesday's Child Is Full of Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRau0OfdR2XiDVJKbosrUguN9E823JJik83QPSbk78xpYSlsuA3ukuo9v2aWKPIHmFCrvcl0kU7jy0mT_FGp1DNg1O0lzZ3VPTrgPhtT4WB2_slAE1osX6d6.webp?r=952", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 7, "Woe Is the Loneliest Number", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcgZnvtaD-K1zxuS9C5dp6zgPRSBHhSjibTDF56qD51O8W4HTNfs2AFlqwtVYtHQHOeupS0ub9EJsu77-lMXe6jaNrvi7t1QUL8FJgseTYRkM7fM5Tdv_JZ4.webp?r=c99", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 8, "Friend or Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABUDKNEB1pBwnu_RXfzP0xnC4k2cz4HulwQ93P4EwhvqT4FXKKhBpljaO4oiDKFmesn0OJbYmmZbXfMzOahtWq0xoWwY0dMXAPTL1zN2DTcfXDYU0r-cglSt6.webp?r=40b", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 9, "Woe What a Night", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABbv6q4nKAIJ7y7bny28gw_Qc_s9kTfFdA8bdTP003iaiw6OhBjjNOfzfMhOgqD7Vk7t7DvtQU9IVhSexDSkk7o4Hn-QTJPKsAEoayYwVEVqEqflcW2Do3Fip.webp?r=0b4", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 10, "You Reap What You Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABf8Aw8Af3JDmZBdCiS5fNLuiEVIhduj5mxtLIXQFaJak0retmAR0_eIqkCXlC-hu2YMQD8lvNItvPLEBvBdLfV8uefPXan2rgjjRuh0DZeIiduerzGqd7iJS.webp?r=e5d", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 11, "Quid Pro Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZf3PiaLYl-ER8sWFeIw2Z560fzeTQmAPviwt3YnGMxDfLg63g8u6aUmsHZZH2Z4o6SBYIRyZ2PMo3fFtnP26nr6PhVMn7nX5NDP_PJ8_bEpEd3Ezfmofxgv.webp?r=21c", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 12, "If You Don't Woe Me by Now", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABXVM0jmBGYrCi9BUQdr1ExNT9LCJn17WCL9KKp9JuKgH6SzZUFpMl0OacUk7XF4zcVVjuU8RIKVF1TdAJCNYVgErGKgn0WwdNq60WGUoqU_kJVLP3TkUlnIr.webp?r=288", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 13, "A Murder of Woes", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZMlXG39lo4-qufHnTdLwQaeI6ku5GLsIVRI1vL7filQJem5OE3QpVXdHGRQP9AmCnWuD8ebtP4MDGqcSC4PKSE2Uvx_FPe9GisatKLnxoLARbpxiJgKrnK2.webp?r=2e0", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0f390bd4-7a7d-4ed2-92a7-6db49306f808") },
                    { 14, "Here We Woe Again", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABaXkU45ZXupNiPrPU8rKYlhydRnhuxOaU5h6Yt2q-Cr3GFtZ0wxgsr0ZA8sxp_AzylnwTAbV2pcVkHv7PNygp1KVpAIb_2-37qW-zLPJR4WlOi_nnmG0IAz9.webp?r=677", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 15, "The Devil You Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZEb-1xChJkJI9ZCXz75XwdSwF-P2n9ScrgvwGbS1fb3LG1BIRnxCmSHw8hgYpE3OIa4Fo-4DQZOoqUeTP5B4OI49QPqLpZCfvICDHLm5Nu1HbhrQR8zMZJx.webp?r=22c", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 16, "Call of the Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcmSOEcPN1IzcvlzRSZjH-Wr4X_BgRKTITYs4ugqKhA_HXPuC1UzFAxQczq26hLnDSiQF6zzvZsfkd3kpDJ5jmjigw6l0wE_AU45uw_um4pPKgBFMDy9B5MR.webp?r=73b", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 17, "If These Woes Could Talk", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZPcezk8r1htr8czZ76-tMP09NbK3lucW-peokfK592jMEGuJVRZuEPny8-y8ysXdPQBq-C2G9xd7d3TrDcU32VB1-yX8gMYdin4hfHfTUTeKLmbhaLS1eGO.webp?r=2b8", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 18, "Hyde and Woe Seek", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRkhY3pujVZ9oaTqAPHzvBuJ477C_u9Gl3COnlpT88wofxxatd6rsMjGruI-5kImm_AFPu3V0fW8gBS_J-1yRFSXTBnprKqgaQquVRWM2SrzH8PQWqDuo-6u.webp?r=a42", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 19, "Woe Thyself", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVyOcT09gIvsvmjeFrx4CdiY8vd277poyEgOxGQz_4SE-MXcYuFjwCPYJjKXp4TvxQ1L59C8jUPxzcBoBRmc4Vveh6FZv3AzrEHv-ttnon53we4RWvkZ66AR.webp?r=069", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 20, "Woe Me The Money", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVLMhYFs43xmPdceyhVIahmCgWeT6CXiI835RV1kXzKKNsG03YlwoUWeSkIxGYegKxMcOIzw1fFrcWQXE6mKr4tYb9P6ktUvtRbYa0e24fqfA0tRASfavopC.webp?r=d3c", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 21, "This Means Woe", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRRYxyq9yQHM2oUDbOKMWwG1-rKXFh0CY5Cb3oG7EBiRK2rStjLQYGKo1-cZd1Rzn1jz9GZPJ4TSf9BZoSoqmx9WbGtbMpIIL2NPPSEa8aHNTmxq4zsTfIH-.webp?r=3cd", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9c524886-a88e-49b3-a510-025b76f4cf27") },
                    { 22, "The Death Row Convict and the Executioner", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZ4Fp6QfStTjje80wxPS_or1bQd0FXCn3BMZFNLTGmtga_LhQgVvuFOpxFDBG3ZDEbf9l0Hg3mTHnC6PE4A6soZawwZ-crB_vChv6N_08HVAy0okT8l2lxYt.webp?r=fc3", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 23, "Screening and Choosing", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcBTzuAj_vSaHDnIqW5MjWc__KAM_NgexKN6I3k4iaXPicwiR-KQzy1yIUt73hjMieEFNYkHM6HPnkiQus66eqoOiR61b6zF3rHKtwRcQKrB6s00uQB-2T_v.webp?r=fab", new DateTime(2023, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 24, "Weakness and Strength", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABa2UG7nvU1KTg8nRVkZDUlMX-mEfdZfAOOzJQJrk4mjIvRYVPYtzHfP-wmkDuBobBnicCBG0zj0FodEHkvU8W3h-9HO9rDy6CmfMAY-hSnpEb9bvuc_l2MKB.webp?r=6c0", new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 25, "Hell and Paradise", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABQzgqr2llHi9H31hsVqc2Zpy21LyPVPqF6TejdZvVD13ypgcAl5eMtVrO5fwiqIRUejC2VSe29uOatbZyPo2BnGpa7nTqXtl-21oeAaG2QeryNkPqs1mcUGh.webp?r=a86", new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 26, "The Samurai and the Woman", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABXh76nHg4o1CXASqP_clIBTdx0Fkk6dLhLE6DJ-zSq_5su0-sLn9TJ8wVnVauB1TlLTX7_UvqqqJC7mYZ7R9pESkNhTPru1B2flvlPFyg6tweRev_D4AUoBS.webp?r=8bb", new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 27, "Heart and Reason", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABfapnc9qA3dfSkpNNNllsxS0tTTcqQ0Yy0pv5UmS1lRzQRMzxSkbvyVHgRqcMY6iSVpLjhIkrc8Z-GT4BEjZrls9U2jGctSDNLGwZmIoaUyEdDqVK56c9p6g.webp?r=394", new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 28, "Flowers and Offerings", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABU4dzDDdtsTASc-LrwfcfjfnPKPoOr57MTb7U4YMMWwYlEtiU7b4kZlgFoOlKp0TTYBxw7wBwYjJzklTV3imArYIF0KhqIbsdBFb1MA5PKgIcFYNBjh-cLXX.webp?r=e8a", new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 29, "Student and Master", "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVVv3wLdl-1UKOdzagRzOIrwvMALEWSB-uIyE9PmlnScku9_OB8Q2Tjf9lgH5aTj60vfLHo7J-HoNuFy5Ux_ZEg8Gf-Y6jqoU6gU5CCooPVfYMRI6OifcKHZ.webp?r=7f6", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 30, "Gods and People", "https://resizing.flixster.com/n91pgJMgin35WqlPC5hd7VyBH2M=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24743367_e_h10_aa.jpg", new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 31, "Yin and Yang", "https://resizing.flixster.com/pbNvgvuGRcNZhZefvhzgM3wC8KA=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857692_e_h10_aa.jpg", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 32, "Weak and Strong", "https://resizing.flixster.com/WlpgXAA-NB960yZwQlBfk2vO1Xg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857696_e_h10_ab.jpg", new DateTime(2023, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 33, "Umbrella and Ink", "https://resizing.flixster.com/4ujEIiHMlz1pAt9dW7nDOcLxt_k=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857701_e_h10_ab.jpg", new DateTime(2023, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 34, "Dreams and Reality", "https://resizing.flixster.com/07LXBRb-Qh69cdaZhVM7wxATHG8=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24509505_e_h10_aa.jpg", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64b81beb-e7dc-409e-a004-0cd2d0442cde") },
                    { 35, "Dawn and Delirium", "https://resizing.flixster.com/gWATwvDMoCBO6RCoslZdjuQcfus=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31864210_e_h10_aa.jpg", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 36, "Reality and Illusion", "https://resizing.flixster.com/_QM8qaO9b9hy7VvlamBnIeiNOSc=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032077_e_h10_aa.jpg", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 37, "Immutability and Change", "https://resizing.flixster.com/Bv2ok4Vydt3GGZ5TmzXaFisfCDs=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032078_e_h10_aa.jpg", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 38, "The Samurai Code and Carnage", "https://resizing.flixster.com/wxn4iO5Fc5xlb0_s3SG0jumAXSU=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032079_e_h10_aa.jpg", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 39, "Humans and Sages", "https://resizing.flixster.com/io6pBBjMHjXxiicmwRgPamc3KUM=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032080_e_h10_aa.jpg", new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 40, "Hindering and Restoration", "https://resizing.flixster.com/2XYMzMtz1MepmLQgBoQAuYrF8jY=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032081_e_h10_aa.jpg", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 41, "Two People and One Person", "https://resizing.flixster.com/WZhJhlnZVv_paJN2B60icqx-dIg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032082_e_h10_aa.jpg", new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 42, "Chrysanthemum and Peach", "https://resizing.flixster.com/eNZMj52gB3hog563e8MofsRSqDU=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032083_e_h10_aa.jpg", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 43, "Love and Karma", "https://resizing.flixster.com/dhmGxLNsRAI0YfayOO0xW1LXhlg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032084_e_h10_aa.jpg", new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 44, "Master and Disciple", "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg", new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 45, "Ephemeralness and Fire", "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") },
                    { 46, "Episode #2.12", "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg", new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("87932afe-1b94-46e6-83a3-034b0aa183be") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_ShowId",
                table: "Seasons",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersEpisodes_EpisodeId",
                table: "UsersEpisodes",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersShows_ShowId",
                table: "UsersShows",
                column: "ShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "UsersEpisodes");

            migrationBuilder.DropTable(
                name: "UsersShows");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
