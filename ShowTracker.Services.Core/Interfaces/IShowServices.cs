using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IShowServices
    {
        bool IsStringNullOrEmpty(string str);

        bool isGuidValid(string str);

        Guid GetGuidFromString(string str);

        Task<Show> GetShowWithSeasonsAndEpisodes(Guid id);

        Task<bool>ShowExistInDatabase(Guid id);
    }
}
