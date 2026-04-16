using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
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
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchShow(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                TempData["ErrorMessage"] = "Search input was empthy.";
                return RedirectToAction("Index","Home");
            }

            List<Show> shows = await exploreServices.GetShowAsync(input);

            if(shows.Count == 0)
            {
                TempData["ErrorMessage"] = "No shows found with that title.";
                return RedirectToAction("Index", "Home");
            }

            return View(nameof(Index), shows);
        }
    }
}
