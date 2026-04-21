using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core
{
    public class EpisodeServices : IEpisodeServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public EpisodeServices(ShowTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteEpisode(Episode episode)
        {
            dbContext.Episodes.Remove(episode);
            await dbContext.SaveChangesAsync();
        }

        public Episode EditEpisode(EditEpisodeViewModel model)
        {
            Episode episode = new Episode()
            {
                Id = model.Id,
                SeasonId = model.SeasonId,
                EpisodeTitle = model.EpisodeTitle,
                ReleaseDate = model.ReleaseDate,
                ImageUrl = model.ImageUrl
            };

            return episode;
        }

        public Task<bool> EpisodeAlreadyWatchedByUser(int id, Guid userId)
        {
            return dbContext.UsersEpisodes.AnyAsync(ue => ue.EpisodeId == id && ue.UserId == userId);
        }

        public async Task<bool> EpisodeExistInDatabase(int id)
        {
            return await dbContext.Episodes.AnyAsync(e => e.Id == id);
        }

        public async Task<Episode?> GetEpisodeWithSeasons(int id)
        {
            return await dbContext.Episodes
                .Include(e => e.Season)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
                
        }

        public async Task WatchedEpisode(int id, Guid userId, bool hasWatched)
        {
            if (!hasWatched)
            {
                UserEpisodes userEpisode = new UserEpisodes()
                {
                    UserId = userId,
                    EpisodeId = id
                };

                dbContext.UsersEpisodes.Add(userEpisode);
                await dbContext.SaveChangesAsync();
            }
            else 
            {
                UserEpisodes userEpisode = dbContext.UsersEpisodes
                    .FirstOrDefault(ue => ue.EpisodeId == id && ue.UserId == userId)!;

                dbContext.UsersEpisodes.Remove(userEpisode);
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task SaveEpisodeChanges(Episode episode)
        {
            dbContext.Episodes.Update(episode);
            await dbContext.SaveChangesAsync();
        }
    }
}
