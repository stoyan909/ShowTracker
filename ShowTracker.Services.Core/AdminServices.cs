using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Services.Core
{
    public class AdminServices : IAdminServices
    {
        private readonly ShowTrackerDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminServices(ShowTrackerDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;

        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(Guid userId)
        {
            return await dbContext.Users.Where(u => u.Id != userId).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser applicationUser)
        {
            IEnumerable<string> userRoles = await userManager.GetRolesAsync(applicationUser);

            return userRoles;
        }

        public IEnumerable<UsersAndRolesViewModel> GetUsersWithRoles(IEnumerable<ApplicationUser> users, IEnumerable<string> roles)
        {
            List<UsersAndRolesViewModel> listOfUser = new List<UsersAndRolesViewModel>();

            foreach (var user in users) 
            {
                 UsersAndRolesViewModel userAndRolesViewModel = new UsersAndRolesViewModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Roles = roles.ToList()
                };

                listOfUser.Add(userAndRolesViewModel);
            }

            return listOfUser;
        }
    }
}
