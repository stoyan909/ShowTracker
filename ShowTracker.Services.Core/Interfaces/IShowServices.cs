using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IShowServices
    {
        Task<IEnumerable<Show>> GetAllShowWithDetailsAsync();

        Task<Show?> GetShowWithDetails(Guid id);

        Task<bool> ShowExistInDatabase(Guid id);

        Task ToggleFollowAsync(Guid showId, Guid userId);

        Task<bool> ShowExistInDatabase(string showTitle);

        Task<bool> UserShowContainsGivenShow(Guid userId, Guid showId);

        Task UnfollowShow(Guid userId, Guid showId);

        Task SaveNewUserShowToDataBase(UsersShows userShow);

        Task<IEnumerable<UsersShows>> GetUsersShowsAsync(Guid userId);
    }
}
