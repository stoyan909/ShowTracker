using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Controllers;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAdminServices adminServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(IAdminServices adminServices, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.adminServices = adminServices;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Guid userId = GetUserId();

            IEnumerable<ApplicationUser> userList = await adminServices.GetAllUsersAsync(userId);

            IEnumerable<UsersAndRolesViewModel> usersAndRoles = mapper.Map<IEnumerable<UsersAndRolesViewModel>>(userList);

            foreach (UsersAndRolesViewModel user in usersAndRoles) 
            {
                ApplicationUser applicationUser = userList.First(u => u.Id != userId);

                user.Roles = await adminServices.GetRolesAsync(applicationUser);
            }

            ViewBag.AllRoles = new string[] { "Admin", "User" };

            return View(usersAndRoles);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(Guid id, string role)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(id.ToString());

            if (user == null) 
            {
                return View("Error");
            }

            IEnumerable<string> currentRoles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, currentRoles);

            await userManager.AddToRoleAsync(user, role);

            TempData["Message"] = "User role updated successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id) 
        {
            ApplicationUser? user = await userManager.FindByIdAsync(id.ToString());

            if (user == null) 
            {
                return View("Error");
            }

            DeleteUserViewModel deleteUser = new DeleteUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id, DeleteUserViewModel model) 
        {
            ApplicationUser? user = await userManager.FindByIdAsync(id.ToString());

            if (user == null) 
            {
                return View("Error");
            }

            try
            {
                await userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");
                return View(model);
            }
        }
    }
}
