using Microsoft.AspNetCore.Mvc;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class Explore : BaseController
    {
        private readonly IExploreServices exploreServices;

        public Explore(IExploreServices exploreServices)
        {
            this.exploreServices = exploreServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await exploreServices.GetAllShowsAsync());
        }
    }
}
