using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public Guid GetGuidFromString(string str)
        {
            Guid showId = Guid.Parse(str);

            return showId;
        }

        public async Task<Show> GetShowWithSeasonsAndEpisodes(Guid id)
        {
             Show show = await dbContext.Shows
                .Where(s => s.Id == id)
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .FirstAsync();

            return show;
        }

        public bool isGuidValid(string str)
        {
            bool guidIsValid = Guid.TryParse(str, out Guid showId);

            return guidIsValid;
        }

        public bool IsStringNullOrEmpty(string str)
        {
            bool isStringNull = str.IsNullOrEmpty();

            return isStringNull;
        }

        public async Task<bool> ShowExistInDatabase(Guid id)
        {
            bool showExist = await dbContext.Shows.AnyAsync(s => s.Id == id);

            return showExist;
        }
    }
}
