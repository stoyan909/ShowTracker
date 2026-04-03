using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Services.Core
{
    public class ShowServices : IShowServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public ShowServices(ShowTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Show CreateShow(CreateShowViewModel showViewModel)
        {
            int season = showViewModel.SeasonNumber;
            Show show = new Show()
            {
                Id = Guid.NewGuid(),
                Name = showViewModel.Name,
                Description = showViewModel.Description,
                Seasons = new List<Season>()
            };

            for (int i = 1; i <= season; i++)
            {
                show.Seasons.Add(new Season()
                {
                    Id = Guid.NewGuid(),
                    SeasonNumber = i,
                    ShowId = show.Id
                });
            }

            return show;
        }

        public async Task DeleteShow(Show show)
        {
            dbContext.Shows.Remove(show);
            await dbContext.SaveChangesAsync();
        }

        public UsersShows FollowShow(string userId, Guid showId)
        {
            UsersShows userShow = new UsersShows()
            {
                UserId = userId,
                ShowId = showId
            };

            return userShow;
        }

        public async Task<Show> GetShowWithSeasonsAndEpisodes(Guid id)
        {
             Show show = await dbContext.Shows
                .Where(s => s.Id == id)
                .Include(s => s.Users)
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .FirstAsync();

            return show;
        }

        public async Task SaveNewShow(Show show)
        {
            dbContext.Shows.Add(show);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveNewUserShowToDataBase(UsersShows userShow)
        {
            dbContext.UsersShows.Add(userShow);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ShowExistInDatabase(Guid id)
        {
            bool showExist = await dbContext.Shows.AnyAsync(s => s.Id == id);

            return showExist;
        }

        public async Task<bool> ShowExistInDatabase(string showTitle)
        {
            bool showExist = await dbContext.Shows.AnyAsync(s => s.Name == showTitle);

            return showExist;
        }

        public async Task UnfollowShow(string userId, Guid showId)
        {
             dbContext.UsersShows.RemoveRange(dbContext.UsersShows.Where(us => us.UserId == userId && us.ShowId == showId));
             await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserShowContainsGivenShow(string userId, Guid showId)
        {
            return await dbContext.UsersShows.AnyAsync(us => us.UserId == userId && us.ShowId == showId);
        }
    }
}
