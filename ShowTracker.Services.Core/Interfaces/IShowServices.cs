using Microsoft.AspNetCore.Http;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IShowServices
    {
        Task<Show?> GetShowWithDetails(Guid id);

        Task<bool> ShowExistInDatabase(Guid id);

        Task ToggleFollowAsync(Guid showId, string userId);

        Task<bool> ShowExistInDatabase(string showTitle);

        Task<bool> UserShowContainsGivenShow(string userId, Guid showId);

        Task GeneratePictureForShow(IFormFile? picture, string name, string id);

        void DeleteShowPicture(Show show);

        Task UnfollowShow(string userId, Guid showId);

        Task SaveNewUserShowToDataBase(UsersShows userShow);

        Task SaveNewShow(Show show);

        Task SaveEditShow(Show show);

        Task DeleteShow(Show show);

        Show AddMultipleSeasonToShow(Show show, int seasons);

        Show AddNewSeasonToShow(Show show);

        Task RemoveLastSeasonFromShow(Show show);
    }
}
