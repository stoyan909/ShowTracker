using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Services.Core
{
    public class ExploreServices : IExploreServices
    {
        private readonly ShowTrackerDbContext dbContext;
        public ExploreServices(ShowTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Show>> GetAllShowsAsync()
        {
            IEnumerable<Show> result = await dbContext.Shows.OrderBy(s => s.Name).Include(s=>s.Users).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Show>> GetShowAsync(string showTitle)
        {
            List<Show> result = await dbContext.Shows
                .Where(s => s.Name.ToLower().Contains(showTitle.ToLower()))
                .OrderBy(s => s.Name)
                .Include(s => s.Users)
                .ToListAsync();

            return result;
        }
    }
}
