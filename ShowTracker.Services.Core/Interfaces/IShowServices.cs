using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IShowServices
    {
        Task<Show> GetShowWithSeasonsAndEpisodes(Guid id);

        Task<bool> ShowExistInDatabase(Guid id);

        Task<bool> UserShowContainsGivenShow(string userId, Guid showId);

        UsersShows FollowShow(string userId, Guid showId);

        Task UnfollowShow(string userId, Guid showId);

        Task SaveNewUserShowToDataBase(UsersShows userShow);
    }
}
