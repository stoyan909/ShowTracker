using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class ExploreController : BaseController
    {
        private readonly IExploreServices exploreServices;

        public ExploreController(IExploreServices exploreServices)
        {
            this.exploreServices = exploreServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await exploreServices.GetAllShowsAsync());
        }
    }
}
