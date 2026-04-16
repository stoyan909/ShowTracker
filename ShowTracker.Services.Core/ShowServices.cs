using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ShowTracker.Services.Core
{
    public class ShowServices : IShowServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public ShowServices(ShowTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Show AddNewSeasonToShow(Show show)
        {
            show.Seasons.Add(
                new Season()
                {
                    ShowId = show.Id,
                    SeasonNumber = show.Seasons.Count + 1
                });

            return show;
        }

        public async Task ToggleFollowAsync(Guid showId, string userId) 
        {

            bool userFollowsGivenShow = await UserShowContainsGivenShow(userId, showId);

            if (!userFollowsGivenShow)
            {
                UsersShows usersShows = new UsersShows
                {
                    UserId = userId,
                    ShowId = showId
                };

                await SaveNewUserShowToDataBase(usersShows);
            }
            else
            {
                await UnfollowShow(userId, showId);
            }
        }

        public Show AddMultipleSeasonToShow(Show show, int seasons)
        {
            for (int i = 1; i <= seasons; i++)
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

        public void DeleteShowPicture(Show show)
        {
            string filePath = $"wwwroot/ShowPics/{show.Name + show.Id}.jpg";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public async Task GeneratePictureForShow(IFormFile? showPictureFile, string name, string id)
        {
            if (showPictureFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShowPics");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = name + id + ".jpg";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var image = await Image.LoadAsync(showPictureFile.OpenReadStream()))
                {
                    await image.SaveAsync(filePath, new JpegEncoder());
                }
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

        public async Task RemoveLastSeasonFromShow(Show show)
        {
            Season? lastSeason = show.Seasons
                .OrderByDescending(s => s.SeasonNumber)
                .FirstOrDefault();
            
            if (lastSeason != null)
            {
                dbContext.Seasons.Remove(lastSeason);
                await dbContext.SaveChangesAsync();   
            }
        }

        public async Task SaveEditShow(Show show)
        {
            dbContext.Shows.Update(show);
            await dbContext.SaveChangesAsync();
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
