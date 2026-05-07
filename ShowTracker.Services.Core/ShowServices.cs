using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Services.Core
{
    public class ShowServices : IShowServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public ShowServices(ShowTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Show>> GetAllShowWithDetailsAsync()
        {
            return await dbContext.Shows
                .Include(s => s.Users)
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .ToListAsync();

        }

        public async Task ToggleFollowAsync(Guid showId, Guid userId)
        {

            bool userFollowsGivenShow = await UserShowContainsGivenShow(userId, showId);

            if (!userFollowsGivenShow)
            {
                UsersShows usersShows = new UsersShows
                {
                    UserId = userId,
                    ShowId = showId,
                    FollowedDate = DateTime.Now
                };

                await SaveNewUserShowToDataBase(usersShows);
            }
            else
            {
                await UnfollowShow(userId, showId);
            }
        }

        public async Task<Show?> GetShowWithDetails(Guid id)
        {
            Show? show = await dbContext.Shows
               .Where(s => s.Id == id)
               .Include(s => s.Users)
               .Include(s => s.Seasons)
               .ThenInclude(s => s.Episodes)
               .ThenInclude(e => e.Users)
               .AsNoTracking()
               .FirstOrDefaultAsync();

            return show;
        }

        public async Task SaveNewUserShowToDataBase(UsersShows userShow)
        {
            await dbContext.UsersShows.AddAsync(userShow);
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

        public async Task UnfollowShow(Guid userId, Guid showId)
        {
            dbContext.UsersShows.RemoveRange(dbContext.UsersShows.Where(us => us.UserId == userId && us.ShowId == showId));
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserShowContainsGivenShow(Guid userId, Guid showId)
        {
            return await dbContext.UsersShows.AnyAsync(us => us.UserId == userId && us.ShowId == showId);
        }

        public async Task<IEnumerable<UsersShows>> GetUsersShowsAsync(Guid userId)
        {
           return await dbContext.UsersShows.Where(us => us.UserId == userId).ToListAsync();
        }
    }
}
