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
        public async Task<IActionResult> CreateSeason(string id, int? count)
        {
            if (generalServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!generalServices.isGuidValid(id))
            {
                return BadRequest();
            }

            Guid showId = generalServices.GetGuidFromString(id);

            bool showExist = await showServices.ShowExistInDatabase(showId);

            if (!showExist)
            {
                return NotFound();
            }

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(showId);

            try
            {
                if (count != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        show = showServices.AddNewSeasonToShow(show);
                    }
                }
                else 
                {
                    show = showServices.AddNewSeasonToShow(show);
                }

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
        public async Task<IActionResult> DeleteSeason(string id, int? count)
        {
            if (generalServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!generalServices.isGuidValid(id))
            {
                return BadRequest();
            }

            Guid showId = generalServices.GetGuidFromString(id);

            bool showExist = await showServices.ShowExistInDatabase(showId);

            if (!showExist)
            {
                return NotFound();
            }

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(showId);

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
                if (count != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        await showServices.RemoveLastSeasonFromShow(show);
                    }
                }
                else
                {
                    await showServices.RemoveLastSeasonFromShow(show);
                }

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
