using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

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

        public async Task<bool> EpisodeExistInDatabase(int id)
        {
            return await dbContext.Episodes.AnyAsync(e => e.Id == id);
        }

        public async Task<Episode> GetEpisodeWithSeasons(int id)
        {
            return await dbContext.Episodes.Include(e=>e.Season).FirstAsync(e => e.Id == id);
        }
    }
}
