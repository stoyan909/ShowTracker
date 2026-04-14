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
            return View(nameof(Index), shows);
        }
    }
}
