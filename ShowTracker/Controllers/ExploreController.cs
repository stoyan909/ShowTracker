using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class ExploreController : BaseController
    {
        private readonly IExploreServices exploreServices;
        private readonly IGeneralServices generalServices;

        public ExploreController(IExploreServices exploreServices, IGeneralServices generalServices)
        {
            this.exploreServices = exploreServices;
            this.generalServices = generalServices;
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
            if (generalServices.IsStringNullOrEmpty(input))
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
