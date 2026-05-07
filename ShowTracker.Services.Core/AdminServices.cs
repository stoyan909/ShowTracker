using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.Admin;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

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

        public async Task RemoveLastSeasonFromShow(Show show, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Season? lastSeason = show.Seasons
                    .OrderByDescending(s => s.SeasonNumber)
                    .FirstOrDefault();

                if (lastSeason != null)
                {
                    dbContext.Seasons.Remove(lastSeason);
                    await dbContext.SaveChangesAsync();
                }

            }
        }

        public async Task SaveEditShow(Show show)
        {
            dbContext.Shows.Update(show);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveNewShow(Show show)
        {
            dbContext.Shows.Add(show);
            await dbContext.SaveChangesAsync();
        }

        public async Task GeneratePictureForShow(IFormFile? showPictureFile, string name, string id)
        {
            if (showPictureFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShowPics");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = name + id + ".jpg";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var image = await Image.LoadAsync(showPictureFile.OpenReadStream()))
                {
                    await image.SaveAsync(filePath, new JpegEncoder());
                }
            }
        }

        public void DeleteShowPicture(Show show)
        {
            string filePath = $"wwwroot/ShowPics/{show.Name + show.Id}.jpg";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public async Task DeleteShow(Show show)
        {
            dbContext.Shows.Remove(show);
            await dbContext.SaveChangesAsync();
        }

        public Show AddMultipleSeasonToShow(Show show, int seasons)
        {
            for (int i = 1; i <= seasons; i++)
            {
                show.Seasons.Add(new Season()
                {
                    Id = Guid.NewGuid(),
                    SeasonNumber = i,
                    ShowId = show.Id
                });
            }

            return show;
        }

        public Show AddNewSeasonToShow(Show show, int count)
        {
            for (int i = 0; i < count; i++)
            {
                show.Seasons.Add(
                    new Season()
                    {
                        ShowId = show.Id,
                        SeasonNumber = show.Seasons.Count + 1
                    });
            }
            return show;
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
