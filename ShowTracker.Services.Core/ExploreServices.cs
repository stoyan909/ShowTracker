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

        public async Task<IEnumerable<Show>> GetRecommendedShowsAsync(Guid userId)
        {
            Dictionary<string, int> usersShowGenreCounter = new Dictionary<string, int>();
            List<Show> recommendedShows = new List<Show>();
            var user = await dbContext.Users.FirstAsync(u => u.Id == userId);
            
            var shows = await dbContext.Shows.Include(s=>s.Genres).ThenInclude(g=>g.Genre).ToListAsync();

            var usersShows = await dbContext.UsersShows.Where(us => us.UserId == userId).Select(us=>us.Show).ToListAsync();

            if (usersShows.Count == 0) 
            {
                return null;
            }

            var genreNames = usersShows.SelectMany(us => us.Genres.Select(g => g.Genre.Name));

            foreach (var genre in genreNames) 
            {
                if (!usersShowGenreCounter.ContainsKey(genre))
                {
                    usersShowGenreCounter.Add(genre, 1);
                }
                else
                {
                    usersShowGenreCounter[genre]++;
                }
            }
               
            usersShowGenreCounter.OrderByDescending(v => v.Value);

            foreach (var k in usersShowGenreCounter.Keys) 
            {
                var recommendedShoww = shows.Where(g => g.Genres.Any(g => g.Genre.Name == k));
            }
            var recommendedShow = shows.Where(g => g.Genres.Any(g=>usersShowGenreCounter.Keys.Contains(g.Genre.Name)));

            foreach (var show in recommendedShow) 
            {
                if (!usersShows.Contains(show)) 
                {
                    recommendedShows.Add(show);
                }
            }

            return recommendedShows;
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
