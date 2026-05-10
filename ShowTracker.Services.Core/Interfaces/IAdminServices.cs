using Microsoft.AspNetCore.Http;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IAdminServices
    {
        Task RemoveLastSeasonFromShow(Show show, int count);

        Task SaveEditShow(Show show);

        Task SaveNewShow(Show show);

        Task GeneratePictureForShow(IFormFile? showPictureFile, string name, string id);

        void DeleteShowPicture(Show show);

        Show AddMultipleSeasonToShow(Show show, int seasons, bool showExist);

        Task DeleteShow(Show show);

        Task<IEnumerable<ApplicationUser>> GetAllUsersExceptCurrentUserAsync(Guid userId);

        Task<IEnumerable<string>> GetRolesAsync(ApplicationUser applicationUser);

        IEnumerable<UsersAndRolesViewModel> GetUsersWithRoles(IEnumerable<ApplicationUser> users, IEnumerable<string> roles);
    }
}
