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

        public async Task<bool> EpisodeExistInDatabase(int id)
        {
            return await dbContext.Episodes.AnyAsync(e => e.Id == id);
        }

        public async Task<Episode> GetEpisodeWithSeasons(int id)
        {
            return await dbContext.Episodes
                .Include(e => e.Season)
                .AsNoTracking()
                .FirstAsync(e => e.Id == id);
                
        }

        public async Task SaveEpisodeChanges(Episode episode)
        {
            dbContext.Episodes.Update(episode);
            await dbContext.SaveChangesAsync();
        }
    }
}
