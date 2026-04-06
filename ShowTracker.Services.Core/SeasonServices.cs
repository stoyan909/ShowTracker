using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core
{
    public class SeasonServices : ISeasonServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public SeasonServices(ShowTrackerDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public Season AddNewEpisodeToSeason(Season season, CreateEpisodeViewModel model)
        {
            Episode episode = new Episode
            {
                Id = season.Episodes.Count() +1,
                EpisodeTitle = model.EpisodeTitle,
                ReleaseDate = model.ReleaseDate,
                ImageUrl = model.ImageUrl,
                SeasonId = model.SeasonId
            };

            season.Episodes.Add(episode);
            return season;
        }

        public async Task<Season> GetSeason(Guid id)
        {
            return await dbContext.Seasons.Where(s => s.Id == id).FirstAsync();
        }

        public async Task SaveSeasonChanges(Season season)
        {
            dbContext.Seasons.Update(season);
            await dbContext.SaveChangesAsync();
        }

        public Task<bool> SeasonExistInDataBase(Guid id)
        {
            return dbContext.Seasons.AnyAsync(s => s.Id == id);
        }
    }
}
