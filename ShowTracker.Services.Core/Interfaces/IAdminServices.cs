using ShowTracker.Data.Models;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IAdminServices
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Guid userId);

        Task<IEnumerable<string>> GetRolesAsync(ApplicationUser applicationUser);

        IEnumerable<UsersAndRolesViewModel> GetUsersWithRoles(IEnumerable<ApplicationUser> users, IEnumerable<string> roles);

        Task<IEnumerable<Show>> GetAllShowWithDetailsAsync();
    }
}
