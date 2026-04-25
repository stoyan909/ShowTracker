using Microsoft.AspNetCore.Mvc;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class ShowController : BaseController
    {
        private readonly IAdminServices adminServices;
        public ShowController(IAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await adminServices.GetAllShowWithDetailsAsync());
        }
    }
}
