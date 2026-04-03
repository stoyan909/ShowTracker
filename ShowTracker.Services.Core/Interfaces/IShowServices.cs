using ShowTracker.Data.Models;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IShowServices
    {
        Task<Show> GetShowWithSeasonsAndEpisodes(Guid id);

        Task<bool> ShowExistInDatabase(Guid id);

        Task<bool> ShowExistInDatabase(string showTitle);

        Task<bool> UserShowContainsGivenShow(string userId, Guid showId);

        UsersShows FollowShow(string userId, Guid showId);

        Task UnfollowShow(string userId, Guid showId);

        Task SaveNewUserShowToDataBase(UsersShows userShow);

        Task SaveNewShow(Show show);

        Task DeleteShow(Show show);

        Show CreateShow(CreateShowViewModel showViewModel);
    }
}
