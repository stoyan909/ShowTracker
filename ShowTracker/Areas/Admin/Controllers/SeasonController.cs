using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class SeasonController : BaseController
    {
        private readonly IShowServices showServices;
        public SeasonController(IShowServices showServices)
        {
            this.showServices = showServices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateSeason(Guid id, int count)
        {

            Show? show = await showServices.GetShowWithDetails(id);

            if (show is null)
            {
                return NotFound();
            }

            show = showServices.AddNewSeasonToShow(show, count);

            try
            {
                await showServices.SaveEditShow(show);

                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = "An error occurred while adding a season to the show.";
                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteSeason(Guid id, int count)
        {
            Show? show = await showServices.GetShowWithDetails(id);

            if (show is null)
            {
                return NotFound();
            }

            if (show.Seasons.Count() < 1)
            {
                TempData["ErrorMessage"] = "The show doesn't have any seasons to delete.";
                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            else if (show.Seasons.Count() == 1)
            {
                TempData["ErrorMessage"] = "Cannot delete the only season. Delete the show instead.";
                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }

            try
            {
                await showServices.RemoveLastSeasonFromShow(show, count);
                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["ErrorMessage"] = "An error occurred while adding a season to the show.";
                return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1 });
            }
        }
    }
}
