using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;

namespace ShowTracker.Services.Tests;

[TestFixture]
public class SeasonServicesTests
{
    private ShowTrackerDbContext dbContext;
    private SeasonServices seasonServices;

    [SetUp]
    public void Setup() 
    {
        var options = new DbContextOptionsBuilder<ShowTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new ShowTrackerDbContext(options);

        seasonServices = new SeasonServices(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }

    [Test]
    public async Task SeasonsExistInDatabase_ReturnTrue_WhenExist() 
    {
        Season season = new Season()
        {
            Id = Guid.NewGuid()
        };

        await dbContext.Seasons.AddAsync(season);
        await dbContext.SaveChangesAsync();

        bool seasonExistInDatabase = await seasonServices.SeasonExistInDataBase(season.Id);

        Assert.True(seasonExistInDatabase);
    }

    [Test]
    public async Task SaveSeasonChanges_ReturnTrue() 
    {
        Season season = new Season()
        {
            Id = Guid.NewGuid(),
            SeasonNumber = 1
        };

        await dbContext.Seasons.AddAsync(season);
        await dbContext.SaveChangesAsync();

        dbContext.Entry(season).State = EntityState.Detached;

        Season season2= new Season()
        {
            Id = season.Id,
            SeasonNumber = 2
        };

        await seasonServices.SaveSeasonChanges(season2);

        Season seasonFromDatabase = await dbContext.Seasons.Where(s => s.Id == season.Id).AsNoTracking().FirstAsync();

        Assert.AreEqual(season2.SeasonNumber,seasonFromDatabase.SeasonNumber);
    }

    [Test]
    public async Task GetSeason_WhenExist() 
    {
        Season season = new Season()
        {
            Id = Guid.NewGuid(),
            SeasonNumber = 1
        };
        await dbContext.Seasons.AddAsync(season);
        await dbContext.SaveChangesAsync();

        Season? seasonFromDatabase = await seasonServices.GetSeason(season.Id);

        Assert.NotNull(seasonFromDatabase);
        Assert.AreEqual(season.Id, seasonFromDatabase.Id);
    }

    [Test]
    public async Task GetSeason_ReturnNull_WhenDosentExist() 
    {
        Season season = new Season()
        {
            Id = Guid.NewGuid(),
            SeasonNumber = 1
        };

        Season? seasonFromDatabase = await seasonServices.GetSeason(season.Id);

        Assert.IsNull(seasonFromDatabase);
    }

    [Test]
    public async Task AddNewEpisodeToSeasonAndSaveToDatabase_ReturnTrue() 
    {
        Season season = new Season()
        {
            Id = Guid.NewGuid(),
            SeasonNumber = 1
        };

        dbContext.Seasons.Add(season);

        Episode episode = new Episode()
        {
            Id = 1,
            EpisodeTitle = "Test"
        };

        await seasonServices.AddNewEpisodeToSeasonAndSaveToDatabase(season, episode);
        Season? seasonFromDatabase = await dbContext.Seasons.Where(s => s.Id == season.Id).Include(s => s.Episodes).FirstOrDefaultAsync();

        Assert.AreEqual(1, seasonFromDatabase.Episodes.Count);
        Assert.AreEqual(episode.Id, seasonFromDatabase.Episodes.First().Id);
    }
}
