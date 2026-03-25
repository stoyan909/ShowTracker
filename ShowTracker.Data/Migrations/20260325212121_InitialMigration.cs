using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShowTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    { 1, "1:23:45", "https://image.tmdb.org/t/p/w500/hlLXt2tOPT6RRnjiUmoxyG1LTFi.jpg", new DateTime(2019, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 2, "Please Remain Calm", "https://image.tmdb.org/t/p/w500/8jv8c8QnXl16rDpD6PEw24kTx5c.jpg", new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 3, "Open Wide, O Earth", "https://image.tmdb.org/t/p/w500/6uBlEXw6p7uXJ9VjA36TObgGJE0.jpg", new DateTime(2019, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 4, "The Happiness of All Mankind", "https://image.tmdb.org/t/p/w500/jyqG0vUj8kT8y9JteLoI0lpBT1s.jpg", new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 5, "Vichnaya Pamyat", "https://image.tmdb.org/t/p/w500/4rWQK9r0p5momkGumZ5qX6Ch12H.jpg", new DateTime(2019, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 6, "Wednesday's Child Is Full of Woe", "https://image.tmdb.org/t/p/w500/9PFonBhy4cQy7Jz20NpMygczOkv.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 7, "Woe Is the Loneliest Number", "https://image.tmdb.org/t/p/w500/eYV6G6c5wPLXnw0lPwQBGzb62LF.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 8, "Friend or Woe", "https://image.tmdb.org/t/p/w500/w0Fao9u1Utn8Hj6kUMlzUxr87hc.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 9, "Woe What a Night", "https://image.tmdb.org/t/p/w500/6z9XqH0I1GtIsfRSAxEPPYUajF2.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 10, "You Reap What You Woe", "https://image.tmdb.org/t/p/w500/3sX5Yucs5cjox96D65gis6pZeRA.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 11, "Quid Pro Woe", "https://image.tmdb.org/t/p/w500/pF3vCtbmZh9rqx8uxFazc2DSES0.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 12, "If You Don't Woe Me by Now", "https://image.tmdb.org/t/p/w500/jPaz6RtWLpmjHtkobaN6D2PfYZ7.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 13, "A Murder of Woes", "https://image.tmdb.org/t/p/w500/n8I2c2MvmP0xSg6u40qCMgfHdCq.jpg", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 14, "Here We Woe Again", "https://image.tmdb.org/t/p/w500/1.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 15, "The Devil You Woe", "https://image.tmdb.org/t/p/w500/2.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 16, "Call of the Woe", "https://image.tmdb.org/t/p/w500/3.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 17, "If These Woes Could Talk", "https://image.tmdb.org/t/p/w500/4.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 18, "Hyde and Woe Seek", "https://image.tmdb.org/t/p/w500/5.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 19, "Woe Thyself", "https://image.tmdb.org/t/p/w500/6.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 20, "Woe Me The Money", "https://image.tmdb.org/t/p/w500/7.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 21, "This Means Woe", "https://image.tmdb.org/t/p/w500/8.jpg", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 22, "The Death Row Convict and the Executioner", "https://image.tmdb.org/t/p/w500/1XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z1.jpg", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 23, "Screening and Choosing", "https://image.tmdb.org/t/p/w500/2XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z2.jpg", new DateTime(2023, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 24, "Weakness and Strength", "https://image.tmdb.org/t/p/w500/3XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z3.jpg", new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 25, "Hell and Paradise", "https://image.tmdb.org/t/p/w500/4XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z4.jpg", new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 26, "The Samurai and the Woman", "https://image.tmdb.org/t/p/w500/5XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z5.jpg", new DateTime(2023, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 27, "Heart and Reason", "https://image.tmdb.org/t/p/w500/6XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z6.jpg", new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 28, "Flowers and Offerings", "https://image.tmdb.org/t/p/w500/7XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z7.jpg", new DateTime(2023, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 29, "Student and Master", "https://image.tmdb.org/t/p/w500/8XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z8.jpg", new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 30, "Gods and People", "https://image.tmdb.org/t/p/w500/9XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z9.jpg", new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 31, "Yin and Yang", "https://image.tmdb.org/t/p/w500/10XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z10.jpg", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 32, "Weak and Strong", "https://image.tmdb.org/t/p/w500/11XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z11.jpg", new DateTime(2023, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 33, "Umbrella and Ink", "https://image.tmdb.org/t/p/w500/12XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z12.jpg", new DateTime(2023, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 34, "Dreams and Reality", "https://image.tmdb.org/t/p/w500/13XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z13.jpg", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 35, "Dawn and Delirium", "https://image.tmdb.org/t/p/w500/a1.jpg", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 36, "Reality and Illusion", "https://image.tmdb.org/t/p/w500/a2.jpg", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 37, "Immutability and Change", "https://image.tmdb.org/t/p/w500/a3.jpg", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 38, "The Samurai Code and Carnage", "https://image.tmdb.org/t/p/w500/a4.jpg", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 39, "Humans and Sages", "https://image.tmdb.org/t/p/w500/a5.jpg", new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 40, "Hindering and Restoration", "https://image.tmdb.org/t/p/w500/a6.jpg", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 41, "Two People and One Person", "https://image.tmdb.org/t/p/w500/a7.jpg", new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 42, "Chrysanthemum and Peach", "https://image.tmdb.org/t/p/w500/a8.jpg", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 43, "Love and Karma", "https://image.tmdb.org/t/p/w500/a9.jpg", new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 44, "Master and Disciple", "https://image.tmdb.org/t/p/w500/a10.jpg", new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 45, "Ephemeralness and Fire", "https://image.tmdb.org/t/p/w500/a11.jpg", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") },
                    { 46, "Episode #2.12", "https://image.tmdb.org/t/p/w500/a12.jpg", new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9d46740-7c4b-4380-8122-adee43c370e8") }
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
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "UsersShows");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
