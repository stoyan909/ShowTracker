using Microsoft.AspNetCore.Http;
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

        Task GeneratePictureForShow(IFormFile? picture, string name, string id);

        void DeleteShowPicture(Show show);

        Task UnfollowShow(Guid userId, Guid showId);

        Task SaveNewUserShowToDataBase(UsersShows userShow);

        Task SaveNewShow(Show show);

        Task SaveEditShow(Show show);

        Task DeleteShow(Show show);

        Show AddMultipleSeasonToShow(Show show, int seasons);

        Show AddNewSeasonToShow(Show show, int count);

        Task RemoveLastSeasonFromShow(Show show, int count);

        Task<IEnumerable<UsersShows>> GetUsersShowsAsync(Guid userId);
    }
}
