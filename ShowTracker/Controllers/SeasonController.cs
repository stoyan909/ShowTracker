using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class SeasonController : Controller
    {
        private readonly IShowServices showServices;
        private readonly IGeneralServices generalServices;
        public SeasonController(IShowServices showServices, IGeneralServices generalServices)
        {
            this.showServices = showServices;
            this.generalServices = generalServices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateSeason(Guid id, int count)
        {

            Show show = await showServices.GetShowWithDetails(id);

            if (show is null)
            {
                return NotFound();
            }

            show = showServices.AddNewSeasonToShow(show, count);

            try
            {
                await showServices.SaveEditShow(show);

                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = "An error occurred while adding a season to the show.";
                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteSeason(Guid id, int count)
        {
            Show show = await showServices.GetShowWithDetails(id);

            if (show is null) 
            {
                return NotFound();
            }

            if (show.Seasons.Count() < 1)
            {
                TempData["ErrorMessage"] = "The show doesn't have any seasons to delete.";
                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            else if (show.Seasons.Count() == 1)
            {
                TempData["ErrorMessage"] = "Cannot delete the only season. Delete the show instead.";
                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }

            try
            {
                await showServices.RemoveLastSeasonFromShow(show, count);
                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = "An error occurred while adding a season to the show.";
                return RedirectToAction("Index", nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
        }
    }
}
