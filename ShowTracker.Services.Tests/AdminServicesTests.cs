using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;
using ShowTracker.ViewModel.Admin;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ShowTracker.Services.Tests;

[TestFixture]
public class AdminServicesTests
{
    private AdminServices adminServices;
    private ShowTrackerDbContext dbContext;
    private UserManager<ApplicationUser> applicationUser;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ShowTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new ShowTrackerDbContext(options);

        adminServices = new AdminServices(dbContext, applicationUser);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }

    [Test]
    public async Task RemoveLastSeasonFromShow_ShouldRemoveMultipleSeason()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>
            {
                new Season { Id = Guid.NewGuid(), SeasonNumber = 1 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 2 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 3 }
            }
        };

        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        await adminServices.RemoveLastSeasonFromShow(show, 2);

        Show? updatedShow = await dbContext.Shows.Include(s => s.Seasons).FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(updatedShow);
        Assert.AreEqual(1, updatedShow.Seasons.Count);
        Assert.IsFalse(updatedShow.Seasons.Any(s => s.SeasonNumber == 3));
        Assert.IsFalse(updatedShow.Seasons.Any(s => s.SeasonNumber == 2));
    }

    [Test]
    public async Task RemoveLastSeasonFromShow_ShouldRemoveLastSeason()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>
            {
                new Season { Id = Guid.NewGuid(), SeasonNumber = 1 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 2 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 3 }
            }
        };

        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        await adminServices.RemoveLastSeasonFromShow(show, 1);

        Show? updatedShow = await dbContext.Shows.Include(s => s.Seasons).FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(updatedShow);
        Assert.AreEqual(2, updatedShow.Seasons.Count);
        Assert.IsFalse(updatedShow.Seasons.Any(s => s.SeasonNumber == 3));
    }

    [Test]
    public async Task RemoveLastSeasonFromShow_ShouldNotRemoveAnySeason()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>
            {
                new Season { Id = Guid.NewGuid(), SeasonNumber = 1 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 2 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 3 }
            }
        };

        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        await adminServices.RemoveLastSeasonFromShow(show, 0);

        Show? updatedShow = await dbContext.Shows.Include(s => s.Seasons).FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(updatedShow);
        Assert.AreEqual(3, updatedShow.Seasons.Count);
        Assert.IsTrue(updatedShow.Seasons.Any(s => s.SeasonNumber == 3));
    }

    [Test]
    public async Task RemoveLastSeasonFromShow_ShouldNotRemoveMoreSeasonsThanExist()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>
            {
                new Season { Id = Guid.NewGuid(), SeasonNumber = 1 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 2 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 3 }
            }
        };
        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        await adminServices.RemoveLastSeasonFromShow(show, 5);

        Show? updatedShow = await dbContext.Shows.Include(s => s.Seasons).FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(updatedShow);
        Assert.AreEqual(0, updatedShow.Seasons.Count);
    }

    [Test]
    public async Task RemoveLastSeasonFromShow_ShouldHandleNullShow()
    {
        Show? show = null;
        Assert.ThrowsAsync<NullReferenceException>(async () => await adminServices.RemoveLastSeasonFromShow(show!, 1));
    }

    [Test]
    public async Task SaveEditShow_ShouldUpdateShowDetails()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original Description"
        };

        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        dbContext.Entry(show).State = EntityState.Detached;

        Show editShow = new Show
        {
            Id = show.Id,
            Name = "Updated Name",
            Description = "Updated Description"
        };

        await adminServices.SaveEditShow(editShow);

        Show? updatedShow = await dbContext.Shows.FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(updatedShow);
        Assert.AreEqual(editShow.Name, updatedShow.Name);
        Assert.AreEqual(editShow.Description, updatedShow.Description);
    }

    [Test]
    public async Task SaveNewShow_True()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "New Show",
            Description = "New Description"
        };

        await adminServices.SaveNewShow(show);

        Show? showFromDatabase = await dbContext.Shows.FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.NotNull(showFromDatabase);
        Assert.AreEqual(show.Id, showFromDatabase.Id);
    }

    [Test]
    public async Task GeneratePictureForShow_ShouldSaveImageWhenFileIsProvided()
    {
        string testName = "TestShow";
        string testId = "123";

        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ShowPics");

        string expectedFileName = testName + testId + ".jpg";
        string expectedFilePath = Path.Combine(uploadsFolder, expectedFileName);


        using var image = new Image<Rgba32>(100, 100);
        using var ms = new MemoryStream();

        await image.SaveAsJpegAsync(ms);
        ms.Position = 0;

        var formFileMock = new Mock<IFormFile>();

        formFileMock.Setup(f => f.OpenReadStream()).Returns(ms);

        await adminServices.GeneratePictureForShow(formFileMock.Object, testName, testId);

        Assert.That(File.Exists(expectedFilePath), Is.True);

        if (File.Exists(expectedFilePath))
        {
            File.Delete(expectedFilePath);
        }
    }

    [Test]
    public async Task GeneratePictureForShow_ShouldNotCreateFileWhenFileIsNull()
    {
        string testName = "TestShow";
        string testId = "123";

        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ShowPics");

        string expectedFileName = testName + testId + ".jpg";
        string expectedFilePath = Path.Combine(uploadsFolder, expectedFileName);

        await adminServices.GeneratePictureForShow(null, testName, testId);

        Assert.That(File.Exists(expectedFilePath), Is.False);
    }
    [Test]
    public void DeleteShowPicture_ShouldDeleteFileWhenFileExists()
    {
        // Arrange
        Show show = new Show
        {
            Name = "TestShow",
            Id = Guid.NewGuid()
        };

        string folderPath = Path.Combine("wwwroot", "ShowPics");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, $"{show.Name + show.Id}.jpg");

        File.WriteAllText(filePath, "test content");

        Assert.That(File.Exists(filePath), Is.True);

        adminServices.DeleteShowPicture(show);

        Assert.That(File.Exists(filePath), Is.False);
    }

    [Test]
    public void DeleteShowPicture_ShouldNotThrowWhenFileDoesNotExist()
    {
        // Arrange
        var show = new Show
        {
            Name = "NonExistingShow",
            Id = Guid.NewGuid()
        };

        string filePath = Path.Combine("wwwroot", "ShowPics", $"{show.Name + show.Id}.jpg");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        Assert.DoesNotThrow(() => adminServices.DeleteShowPicture(show));
    }

    [Test]
    public async Task DeleteShow_ShouldDeleteShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description"
        };

        await dbContext.Shows.AddAsync(show);
        await dbContext.SaveChangesAsync();

        await adminServices.DeleteShow(show);

        Show? deletedShow = await dbContext.Shows.FirstOrDefaultAsync(s => s.Id == show.Id);

        Assert.IsNull(deletedShow);
    }

    [Test]
    public async Task DeleteShow_ShouldDoNothing()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description"
        };

        Assert.DoesNotThrow(() =>
        {
            adminServices.DeleteShow(show);
        });
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddMultipleSeasonsToExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = 3;
        bool showExist = true;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(seasonsToAdd, updatedShow.Seasons.Count);

        for (int i = 1; i <= seasonsToAdd; i++)
        {
            Assert.IsTrue(updatedShow.Seasons.Any(s => s.SeasonNumber == i));
        }
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddZeroSeasonsToExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = 0;
        bool showExist = true;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(0, updatedShow.Seasons.Count);
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddNegativeSeasonsToExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = -3;
        bool showExist = true;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(0, updatedShow.Seasons.Count);
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddMultipleSeasonsToNonExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = 3;
        bool showExist = false;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(seasonsToAdd, updatedShow.Seasons.Count);

        for (int i = 1; i <= seasonsToAdd; i++)
        {
            Assert.IsTrue(updatedShow.Seasons.Any(s => s.SeasonNumber == i));
        }
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddZeroSeasonsToNonExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = 0;
        bool showExist = false;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(0, updatedShow.Seasons.Count);
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddNegativeSeasonsToNonExistingShow()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>()
        };

        int seasonsToAdd = -3;
        bool showExist = false;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, showExist);

        Assert.AreEqual(0, updatedShow.Seasons.Count);
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldHandleNullShow()
    {
        Show? show = null;
        Assert.Throws<NullReferenceException>(() => adminServices.AddMultipleSeasonToShow(show!, 3, false));
    }

    [Test]
    public async Task AddMultipleSeasonToShow_ShouldAddSeasonsToExistingSeasons()
    {
        Show show = new Show
        {
            Id = Guid.NewGuid(),
            Name = "Test Show",
            Description = "Test Description",
            Seasons = new List<Season>
            {
                new Season { Id = Guid.NewGuid(), SeasonNumber = 1 },
                new Season { Id = Guid.NewGuid(), SeasonNumber = 2 }
            }
        };

        int seasonsToAdd = 2;

        Show updatedShow = adminServices.AddMultipleSeasonToShow(show, seasonsToAdd, true);

        Assert.AreEqual(4, updatedShow.Seasons.Count);

        for (int i = 1; i <= 4; i++)
        {
            Assert.IsTrue(updatedShow.Seasons.Any(s => s.SeasonNumber == i));
        }
    }

    [Test]
    public async Task GetAllUsersExceptCurrentUserAsync_ReturnsAllUsers()
    {
        Guid user1Id = Guid.NewGuid();
        Guid user2Id = Guid.NewGuid();
        Guid user3Id = Guid.NewGuid();

        List<ApplicationUser> users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = user1Id, UserName = "user1" },
            new ApplicationUser { Id = user2Id, UserName = "user2" },
            new ApplicationUser { Id = user3Id, UserName = "user3" }
        };

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();

        IEnumerable<ApplicationUser> allUser = await adminServices.GetAllUsersExceptCurrentUserAsync(user1Id);

        Assert.AreEqual(2, allUser.Count());
        Assert.True(allUser.Any(u => u.Id == user2Id));
    }

    [Test]
    public void GetUsersWithRoles_Should_Return_Users_With_Roles()
    {
        Guid user1Id = Guid.NewGuid();
        Guid user2Id = Guid.NewGuid();

        List<ApplicationUser> users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = user1Id, Email = "user1@test.com" },
            new ApplicationUser { Id = user2Id, Email = "user2@test.com" }
        };

        List<string> roles = new List<string>
        {
        "Admin",
        "Moderator"
        };

        IEnumerable<UsersAndRolesViewModel> result = adminServices.GetUsersWithRoles(users, roles);

        List<UsersAndRolesViewModel> resultList = result.ToList();

        Assert.That(resultList.Count, Is.EqualTo(2));

        Assert.That(resultList[0].Email, Is.EqualTo("user1@test.com"));
        Assert.That(resultList[0].Id, Is.EqualTo(user1Id));
        Assert.That(resultList[0].Roles,
            Is.EquivalentTo(roles));

        Assert.That(resultList[1].Email, Is.EqualTo("user2@test.com"));
        Assert.That(resultList[1].Id, Is.EqualTo(user2Id));
        Assert.That(resultList[1].Roles,
            Is.EquivalentTo(roles));
    }
}
