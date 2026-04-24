using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAdminServices adminServices;
        private readonly IMapper mapper;

        public UserController(IAdminServices adminServices, IMapper mapper)
        {
            this.adminServices = adminServices;
            this.mapper = mapper;
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
    }
}
