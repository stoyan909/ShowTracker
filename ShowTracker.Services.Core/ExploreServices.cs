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

        public async Task<List<Show>> GetAllShowsAsync()
        {
            List<Show> result = await dbContext.Shows.OrderBy(s => s.Name).Include(s=>s.Users).ToListAsync();
            return result;
        }
    }
}
