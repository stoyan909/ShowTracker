using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;

namespace ShowTracker.Services.Tests
{
    [TestFixture]
    public class ShowServicesTests
    {
        private ShowTrackerDbContext dbContext;
        private ShowServices showServices;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ShowTrackerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new ShowTrackerDbContext(options);

            showServices = new ShowServices(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllShowWithDetailsAsync_ReturnsShowsWithDetails()
        {
            Guid showId = Guid.NewGuid();
            Guid seasonId = Guid.NewGuid();

            UsersShows userShow = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = showId

            };

            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = seasonId,
                EpisodeTitle = "Test episode",
                ReleaseDate = new DateTime(2024, 1, 1),
            };

            Season season = new Season()
            {
                Id = seasonId,
                ShowId = showId,
                SeasonNumber = 1,
                Episodes = new List<Episode>() { episode }
            };

            Show show = new Show()
            {
                Id = showId,
                Name = "Test show",
                Description = "Test description",
                Users = new List<UsersShows>() { userShow },
                Seasons = new List<Season>() { season }
            };

            await dbContext.Shows.AddAsync(show);
            await dbContext.SaveChangesAsync();

            IEnumerable<Show> showFromDatabase = await showServices.GetAllShowWithDetailsAsync();
            Assert.AreEqual(1, showFromDatabase.Count());
            Assert.AreEqual(showId, showFromDatabase.First().Id);
            Assert.AreEqual(seasonId, showFromDatabase.First().Seasons.First().Id);
            Assert.AreEqual(userShow, showFromDatabase.First().Users.First());

        }

        [Test]
        public async Task GetAllShowWithDetailsAsync_ReturnsEmptyListWhenNoShows()
        {
            IEnumerable<Show> showFromDatabase = await showServices.GetAllShowWithDetailsAsync();
            Assert.IsEmpty(showFromDatabase);
        }

        [Test]
        public async Task ToggleFollowAsync_WhenUserFollowsShow_AddsUserShow()
        {
            Guid showId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            Show show = new Show()
            {
                Id = showId,
                Name = "Test show",
                Description = "Test description",
                Users = new List<UsersShows>(),
                Seasons = new List<Season>()
            };

            await dbContext.Shows.AddAsync(show);
            await dbContext.SaveChangesAsync();

            await showServices.ToggleFollowAsync(showId, userId);

            UsersShows? userShowFromDatabase = await dbContext.UsersShows.FirstOrDefaultAsync(us => us.ShowId == showId && us.UserId == userId);

            Assert.IsNotNull(userShowFromDatabase);
            Assert.AreEqual(showId, userShowFromDatabase.ShowId);
            Assert.AreEqual(userId, userShowFromDatabase.UserId);
        }

        [Test]
        public async Task ToggleFollowAsync_WhenUserUnfollowsShow_RemovesUserShow()
        {
            Guid showId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            UsersShows userShow = new UsersShows()
            {
                UserId = userId,
                ShowId = showId
            };

            Show show = new Show()
            {
                Id = showId,
                Name = "Test show",
                Description = "Test description",
                Users = new List<UsersShows>() { userShow },
                Seasons = new List<Season>()
            };

            await dbContext.Shows.AddAsync(show);
            await dbContext.SaveChangesAsync();
            await showServices.ToggleFollowAsync(showId, userId);

            UsersShows? userShowFromDatabase = await dbContext.UsersShows.FirstOrDefaultAsync(us => us == userShow);

            Assert.IsNull(userShowFromDatabase);

        }

        [Test]
        public async Task GetShowWithDetailsAsync_ReturnsShowWithDetails()
        {
            Guid showId = Guid.NewGuid();
            Guid seasonId = Guid.NewGuid();
            int episodeId = 1;

            UsersShows userShow = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = showId

            };

            UserEpisodes userEpisode = new UserEpisodes()
            {
                UserId = userShow.UserId,
                EpisodeId = episodeId,
            };

            Episode episode = new Episode()
            {
                Id = episodeId,
                SeasonId = seasonId,
                EpisodeTitle = "Test episode",
                ReleaseDate = new DateTime(2024, 1, 1),
                Users = new List<UserEpisodes>() { userEpisode }
            };

            Season season = new Season()
            {
                Id = seasonId,
                ShowId = showId,
                SeasonNumber = 1,
                Episodes = new List<Episode>() { episode }
            };

            Show show = new Show()
            {
                Id = showId,
                Name = "Test show",
                Description = "Test description",
                Users = new List<UsersShows>() { userShow },
                Seasons = new List<Season>() { season }
            };

            await dbContext.Shows.AddAsync(show);
            await dbContext.SaveChangesAsync();

            Show? showFromDatabase = await showServices.GetShowWithDetails(showId);

            Assert.IsNotNull(showFromDatabase);
            Assert.AreEqual(showId, showFromDatabase.Id);
            Assert.AreEqual(seasonId, showFromDatabase.Seasons.First().Id);
            Assert.AreEqual(userShow.User, showFromDatabase.Users.First().User);
            Assert.AreEqual(episodeId, showFromDatabase.Seasons.First().Episodes.First().Id);
            Assert.AreEqual(userEpisode.User, showFromDatabase.Seasons.First().Episodes.First().Users.First().User);
        }

        [Test]
        public async Task GetShowWithDetailsAsync_ReturnsEmptyWhenNoShowFound()
        {
            Guid showId = Guid.NewGuid();

            Show showFromDatabase = await showServices.GetShowWithDetails(showId);
            Assert.IsNull(showFromDatabase);
        }

        [Test]
        public async Task SaveNewUserShowToDataBase_ShouldSaveUserShow()
        {
            UsersShows usersShows = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = Guid.NewGuid(),
            };

            await showServices.SaveNewUserShowToDataBase(usersShows);

            UsersShows? usersShowsFromDatabase = await dbContext.UsersShows.FirstOrDefaultAsync(us => us == usersShows);

            Assert.IsNotNull(usersShowsFromDatabase);
        }

        [Test]
        public async Task ShowExistInDatabase_ShouldReturnsTrue()
        {
            Show show = new Show()
            {
                Id = Guid.NewGuid(),
                Name = "Test show",
                Description = "Description",
                Seasons = new List<Season>() { },
                Users = new List<UsersShows>() { }
            };

            await dbContext.Shows.AddAsync(show);
            await dbContext.SaveChangesAsync();

            bool showExistById = await showServices.ShowExistInDatabase(show.Id);
            bool showExistByName = await showServices.ShowExistInDatabase(show.Name);

            Assert.True(showExistById);
            Assert.True(showExistByName);
        }

        [Test]
        public async Task ShowExistInDatabase_ShouldReturnsFalse()
        {
            Guid showId = Guid.NewGuid();
            string showName = "Show name";

            bool showExistById = await showServices.ShowExistInDatabase(showId);
            bool showExistByName = await showServices.ShowExistInDatabase(showName);

            Assert.False(showExistById);
            Assert.False(showExistByName);
        }

        [Test]
        public async Task UnfollowShow_RemoveUserShowsFromDatabase()
        {
            UsersShows usersShows = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = Guid.NewGuid(),
            };

            await dbContext.UsersShows.AddAsync(usersShows);
            await dbContext.SaveChangesAsync();

            await showServices.UnfollowShow(usersShows.UserId, usersShows.ShowId);

            UsersShows? usersShowsFromDatbase = await dbContext.UsersShows.FirstOrDefaultAsync(us => us == usersShows);

            Assert.IsNull(usersShowsFromDatbase);
        }

        [Test]
        public async Task UserShowContainsGivenShow_ShouldReturnTrueIfUserShowExistInDatabase()
        {
            UsersShows usersShows = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = Guid.NewGuid(),
            };

            await dbContext.UsersShows.AddAsync(usersShows);
            await dbContext.SaveChangesAsync();

            bool userShowExistInDatbase = await showServices.UserShowContainsGivenShow(usersShows.UserId, usersShows.ShowId);

            Assert.True(userShowExistInDatbase);
        }

        [Test]
        public async Task UserShowContainsGivenShow_ShouldReturnFalseIfUserShowDosentExistInDatabase()
        {
            UsersShows usersShows = new UsersShows()
            {
                UserId = Guid.NewGuid(),
                ShowId = Guid.NewGuid(),
            };

            bool userShowExistInDatbase = await showServices.UserShowContainsGivenShow(usersShows.UserId, usersShows.ShowId);

            Assert.False(userShowExistInDatbase);
        }

        [Test]
        public async Task GetUsersShowsAsync_ShouldReturnUsersShows()
        {
            Guid userId = Guid.NewGuid();
            Guid firstShowId = Guid.NewGuid();
            Guid secondShowId = Guid.NewGuid();

            List<UsersShows> usersShows = new List<UsersShows>()
            {
                new UsersShows
                {
                    UserId = userId,
                    ShowId = firstShowId
                },

                new UsersShows
                {
                    UserId = userId,
                    ShowId = secondShowId
                },
            };

            await dbContext.UsersShows.AddRangeAsync(usersShows);
            await dbContext.SaveChangesAsync();

            IEnumerable<UsersShows> usersShowsFromDatabse = await showServices.GetUsersShowsAsync(userId);

            Assert.AreEqual(2, usersShowsFromDatabse.Count());
            Assert.True(usersShowsFromDatabse.Any(us => us.ShowId == firstShowId));
            Assert.True(usersShowsFromDatabse.Any(us => us.ShowId == secondShowId));
        }

        [Test]
        public async Task GetUsersShowsAsync_ReturnEmpthyWhenUserIdNotExist()
        {
            Guid userId = Guid.NewGuid();

            List<UsersShows> usersShows = new List<UsersShows>()
            {
                new UsersShows
                {
                    UserId = userId,
                    ShowId = Guid.NewGuid(),
                },
            };
            Guid nonExistingUserId = Guid.NewGuid();

            await dbContext.UsersShows.AddRangeAsync(usersShows);
            await dbContext.SaveChangesAsync();

            IEnumerable<UsersShows> usersShowsFromDatabse = await showServices.GetUsersShowsAsync(nonExistingUserId);

            Assert.IsEmpty(usersShowsFromDatabse);
        }
    }
}
