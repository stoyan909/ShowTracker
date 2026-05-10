using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Tests
{
    [TestFixture]
    public class EpisodeServicesTests
    {
        private EpisodeServices episodeServices;
        private ShowTrackerDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ShowTrackerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new ShowTrackerDbContext(options);

            episodeServices = new EpisodeServices(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task DeleteEpisode_RemoveEpisodeFromDatabase()
        {
            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = Guid.NewGuid(),
                EpisodeTitle = "Test Episode",
                ReleaseDate = DateTime.Now,
            };

            dbContext.Episodes.Add(episode);
            await dbContext.SaveChangesAsync();

            await episodeServices.DeleteEpisode(episode);

            Assert.AreEqual(0, await dbContext.Episodes.CountAsync());
            Assert.False(dbContext.Episodes.Contains(episode));
        }

        [Test]
        public void EditEpisode_ReturnsEpisodeFromEditEpisodeViewModel()
        {
            Season season = new Season()
            {
                Id = Guid.NewGuid(),
                ShowId = Guid.NewGuid(),
                SeasonNumber = 1
            };

            EditEpisodeViewModel model = new EditEpisodeViewModel()
            {
                Id = 1,
                SeasonId = season.Id,
                EpisodeTitle = "Edited Episode",
                ReleaseDate = new DateTime(2024, 1, 1),
                ImageUrl = "http://example.com/image.jpg"
            };

            Episode episode = episodeServices.EditEpisode(model);

            Assert.AreEqual(episode.Id, model.Id);
            Assert.AreEqual(episode.SeasonId, model.SeasonId);
            Assert.AreEqual(episode.EpisodeTitle, model.EpisodeTitle);
            Assert.AreEqual(episode.ReleaseDate, model.ReleaseDate);
            Assert.AreEqual(episode.ImageUrl, model.ImageUrl);
        }

        [Test]
        public async Task EpisodeAlreadyWatchedByUser_ReturnsTrueIfEpisodeWatched()
        {
            Guid userId = Guid.NewGuid();

            int episodeId = 1;

            UserEpisodes userEpisode = new UserEpisodes()
            {
                UserId = userId,
                EpisodeId = episodeId
            };

            await dbContext.UsersEpisodes.AddAsync(userEpisode);
            await dbContext.SaveChangesAsync();

            bool result = await episodeServices.EpisodeAlreadyWatchedByUser(episodeId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EpisodeAlreadyWatchedByUser_ReturnsFalseIfEpisodeNotWatched()
        {
            Guid userId = Guid.NewGuid();

            int episodeId = 1;

            bool result = await episodeServices.EpisodeAlreadyWatchedByUser(episodeId, userId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EpisodeExistInDatabase_ReturnsTrueIfEpisodeExists()
        {
            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = Guid.NewGuid(),
                EpisodeTitle = "Test Episode",
                ReleaseDate = DateTime.Now,
            };

            await dbContext.Episodes.AddAsync(episode);
            await dbContext.SaveChangesAsync();

            bool result = await episodeServices.EpisodeExistInDatabase(episode.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EpisodeExistInDatabase_ReturnsFalseIfEpisodeDosentExists()
        {
            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = Guid.NewGuid(),
                EpisodeTitle = "Test Episode",
                ReleaseDate = DateTime.Now,
            };

            await dbContext.Episodes.AddAsync(episode);
            await dbContext.SaveChangesAsync();

            bool result = await episodeServices.EpisodeExistInDatabase(episode.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetEpisodeWithSeasons_ReturnsEpisodeWithSeasonIfIdMatch()
        {
            Guid seasonId = Guid.NewGuid();

            Season season = new Season()
            {
                Id = seasonId,
                ShowId = Guid.NewGuid(),
                SeasonNumber = 1
            };

            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = seasonId,
                EpisodeTitle = "Test Episode",
                ReleaseDate = DateTime.Now,
            };

            await dbContext.Seasons.AddAsync(season);
            await dbContext.Episodes.AddAsync(episode);
            await dbContext.SaveChangesAsync();

            Episode? result = await episodeServices.GetEpisodeWithSeasons(episode.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(episode.Id, result.Id);
            Assert.AreEqual(season.Id, result.Season.Id);
        }

        [Test]
        public async Task GetEpisodeWithSeasons_ReturnsNullIfNoIdMatch()
        {
            Guid seasonId = Guid.NewGuid();

            Season season = new Season()
            {
                Id = seasonId,
                ShowId = Guid.NewGuid(),
                SeasonNumber = 1
            };

            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = seasonId,
                EpisodeTitle = "Test Episode",
                ReleaseDate = DateTime.Now,
            };

            await dbContext.Seasons.AddAsync(season);
            await dbContext.Episodes.AddAsync(episode);
            await dbContext.SaveChangesAsync();

            Episode? result = await episodeServices.GetEpisodeWithSeasons(2);

            Assert.IsNull(result);
        }

        [Test]
        public async Task WatchedEpisode_RemovesUserEpisodeIfHasWatchedIsTrue()
        {
            Guid userId = Guid.NewGuid();

            int episodeId = 1;

            UserEpisodes userEpisode = new UserEpisodes()
            {
                UserId = userId,
                EpisodeId = episodeId
            };

            await dbContext.UsersEpisodes.AddAsync(userEpisode);
            await dbContext.SaveChangesAsync();

            await episodeServices.WatchedEpisode(episodeId, userId, true);

            UserEpisodes? userEpisodeFromDatabase = await dbContext.UsersEpisodes.FirstOrDefaultAsync(ue => ue.EpisodeId == episodeId && ue.UserId == userId);

            Assert.IsNull(userEpisodeFromDatabase);
            Assert.AreEqual(0, dbContext.UsersEpisodes.Count());
        }

        [Test]
        public async Task WatchedEpisode_AddsUserEpisodeIfHasWatchedIsFalse()
        {
            Guid userId = Guid.NewGuid();

            int episodeId = 1;

            await episodeServices.WatchedEpisode(episodeId, userId, false);

            UserEpisodes? userEpisodeFromDatabase = await dbContext.UsersEpisodes.FirstOrDefaultAsync(ue => ue.EpisodeId == episodeId && ue.UserId == userId);

            Assert.IsNotNull(userEpisodeFromDatabase);
            Assert.AreEqual(userId, userEpisodeFromDatabase.UserId);
            Assert.AreEqual(episodeId, userEpisodeFromDatabase.EpisodeId);
            Assert.AreEqual(1, dbContext.UsersEpisodes.Count());
        }

        [Test]
        public async Task SaveEpisodeChanges_False()
        {

            Episode episode = new Episode
            {
                Id = 999,
                SeasonId = Guid.NewGuid(),
                EpisodeTitle = "Non-existing",
                ReleaseDate = new DateTime(2000, 1, 1)
            };

            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () =>
                await episodeServices.SaveEpisodeChanges(episode));
        }

        [Test]
        public async Task SaveEpisodeChanges_True()
        {
            Episode episode = new Episode()
            {
                Id = 1,
                SeasonId = Guid.NewGuid(),
                EpisodeTitle = "Test Episode",
                ReleaseDate = new DateTime(1999, 1, 1),
            };

            await dbContext.Episodes.AddAsync(episode);
            await dbContext.SaveChangesAsync();

            dbContext.Entry(episode).State = EntityState.Detached;

            Episode editEpisode = new Episode()
            {
                Id = episode.Id,
                SeasonId = episode.SeasonId,
                EpisodeTitle = "New Test Episode",
                ReleaseDate = new DateTime(2000, 1, 1),
            };

            await episodeServices.SaveEpisodeChanges(editEpisode);

            Episode? episodeFromDatabase = await dbContext.Episodes.FirstOrDefaultAsync(e => e.Id == episode.Id);

            Assert.IsNotNull(episodeFromDatabase);
            Assert.AreEqual(episode.Id, episodeFromDatabase.Id);
            Assert.AreEqual(editEpisode.EpisodeTitle, episodeFromDatabase.EpisodeTitle);
            Assert.AreEqual(editEpisode.ReleaseDate, episodeFromDatabase.ReleaseDate);
        }

        [Test]
        public async Task TotalEpisodesOfShowWatchedAsync_ReturnsCorrectCount()
        {
            Guid userId = Guid.NewGuid();

            Guid showId = Guid.NewGuid();

            Season season = new Season()
            {
                Id = Guid.NewGuid(),
                ShowId = showId,
                SeasonNumber = 1
            };

            Episode episode1 = new Episode()
            {
                Id = 1,
                SeasonId = season.Id,
                EpisodeTitle = "Episode 1",
                ReleaseDate = new DateTime(2024, 1, 1),
            };

            Episode episode2 = new Episode()
            {
                Id = 2,
                SeasonId = season.Id,
                EpisodeTitle = "Episode 2",
                ReleaseDate = new DateTime(2024, 1, 10),
            };

            Episode episode3 = new Episode()
            {
                Id = 3,
                SeasonId = season.Id,
                EpisodeTitle = "Episode 3",
                ReleaseDate = new DateTime(2024, 1, 17),
            };

            await dbContext.Seasons.AddAsync(season);

            await dbContext.Episodes.AddRangeAsync(episode1, episode2, episode3);

            await dbContext.UsersEpisodes.AddRangeAsync(
                new UserEpisodes() { UserId = userId, EpisodeId = episode1.Id },
                new UserEpisodes() { UserId = userId, EpisodeId = episode3.Id }
            );

            await dbContext.SaveChangesAsync();

            int totalWatchedEpisodes = await episodeServices.TotalEpisodesOfShowWatchedAsync(showId, userId);

            Assert.AreEqual(2, totalWatchedEpisodes);
        }
    }
}
