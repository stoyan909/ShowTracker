using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IExploreServices
    {
        Task<IEnumerable<Show>> GetAllShowsAsync();
        Task<IEnumerable<Show>> GetShowAsync(string showTitle);
    }
}
