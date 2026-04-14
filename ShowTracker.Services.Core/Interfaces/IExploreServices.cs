using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IExploreServices
    {
        Task<List<Show>> GetAllShowsAsync();
        Task<List<Show>> GetShowAsync(string showTitle);
    }
}
