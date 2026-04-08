using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Controllers
{
    public class EpisodeController : BaseController
    {
        private readonly ISeasonServices seasonServices;
        private readonly IGeneralServices generalServices;
        public EpisodeController(ISeasonServices seasonServices, IGeneralServices generalServices)
        {
            this.seasonServices = seasonServices;
            this.generalServices = generalServices;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewEpisode(string id)
        {
            if (generalServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!generalServices.isGuidValid(id))
            {
                return BadRequest();
            }

            Guid seasonId = generalServices.GetGuidFromString(id);

            bool seasonExist = await seasonServices.SeasonExistInDataBase(seasonId);

            if (!seasonExist)
            {
                return NotFound();
            }

            CreateEpisodeViewModel model = new CreateEpisodeViewModel
            {
                SeasonId = seasonId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEpisode(CreateEpisodeViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool seasonExist = await seasonServices.SeasonExistInDataBase(model.SeasonId);

            if (!seasonExist)
            {
                return NotFound();
            }

            Season season = await seasonServices.GetSeason(model.SeasonId);

            try
            {
                seasonServices.AddNewEpisodeToSeason(season, model);

                await seasonServices.SaveSeasonChanges(season);

                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = season.ShowId, seasonNumber = season.SeasonNumber});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the season.");
                return View(model);
            }
        }
    }
}
