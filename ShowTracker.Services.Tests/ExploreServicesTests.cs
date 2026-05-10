using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;

namespace ShowTracker.Services.Tests;

[TestFixture]
public class ExploreServicesTests
{
    private ExploreServices exploreServices;
    private ShowTrackerDbContext dbContext;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ShowTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new ShowTrackerDbContext(options);

        exploreServices = new ExploreServices(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }

    [Test]
    public async Task GetAllShowsAsync_ReturnsListOfShows()
    {
        Show show = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Test Show 1",
            Description = "This is a test show."
        };

        Show show2 = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Test Show 2",
            Description = "This is another test show."
        };

        dbContext.Shows.Add(show);
        dbContext.Shows.Add(show2);
        dbContext.SaveChanges();

        IEnumerable<Show> showsFromDatabase = await exploreServices.GetAllShowsAsync();

        Assert.AreEqual(2, showsFromDatabase.Count());
        Assert.That(showsFromDatabase.Contains(show));
        Assert.That(showsFromDatabase.Contains(show2));
    }

    [Test]

    public async Task GetAllShowsAsync_ReturnsEmptyListWhenNoShows()
    {
        IEnumerable<Show> showsFromDatabase = await exploreServices.GetAllShowsAsync();
        Assert.AreEqual(0, showsFromDatabase.Count());
    }

    [Test]
    public async Task GetShowAsync_ReturnsTrueForMatchingTitle()
    {
        Show show = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Test Show 1",
            Description = "This is a test show."
        };
        Show show2 = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Another Test Show",
            Description = "This is another test show."
        };

        dbContext.Shows.Add(show);
        dbContext.Shows.Add(show2);
        dbContext.SaveChanges();

        IEnumerable<Show> showsFromDatabase = await exploreServices.GetShowAsync("Test Show 1");
        Assert.AreEqual(1, showsFromDatabase.Count());
        Assert.That(showsFromDatabase.Contains(show));
    }

    [Test]
    public async Task GetShowAsync_ReturnsFalseForMatchingTitle()
    {
        Show show = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Test Show 1",
            Description = "This is a test show."
        };
        Show show2 = new Show()
        {
            Id = Guid.NewGuid(),
            Name = "Another Test Show",
            Description = "This is another test show."
        };

        dbContext.Shows.Add(show);
        dbContext.Shows.Add(show2);
        dbContext.SaveChanges();

        IEnumerable<Show> showsFromDatabase = await exploreServices.GetShowAsync("Test Show 3");
        Assert.AreEqual(0, showsFromDatabase.Count());
        Assert.False(showsFromDatabase.Contains(show));
        Assert.False(showsFromDatabase.Contains(show2));
    }
}
