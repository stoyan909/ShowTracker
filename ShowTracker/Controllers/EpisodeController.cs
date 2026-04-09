using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Controllers
{
    public class EpisodeController : BaseController
    {
        private readonly ISeasonServices seasonServices;
        private readonly IGeneralServices generalServices;
        private readonly IEpisodeServices episodeServices;
        public EpisodeController(ISeasonServices seasonServices, IGeneralServices generalServices, IEpisodeServices episodeServices)
        {
            this.seasonServices = seasonServices;
            this.generalServices = generalServices;
            this.episodeServices = episodeServices;
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
                await seasonServices.AddNewEpisodeToSeasonAndSaveToDatabase(season, model);

                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = season.ShowId, seasonNumber = season.SeasonNumber});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the season.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditEpisode(string id)
        {

            if (generalServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!generalServices.isIntValid(id))
            {
                return BadRequest();
            }

            int episodeId = generalServices.GetIntFromString(id);

            bool episodeExist = await episodeServices.EpisodeExistInDatabase(episodeId);

            if (!episodeExist)
            {
                return NotFound();
            }

            Episode episode = await episodeServices.GetEpisodeWithSeasons(episodeId);

            EditEpisodeViewModel model = new EditEpisodeViewModel()
            {
                Id = episode.Id,
                SeasonId = episode.SeasonId,
                EpisodeTitle = episode.EpisodeTitle,
                ReleaseDate = episode.ReleaseDate,
                Season = episode.Season,
                ImageUrl = episode.ImageUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEpisode(EditEpisodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool episdeExist = await episodeServices.EpisodeExistInDatabase(model.Id);

            if (!episdeExist)
            {
                return NotFound();
            }

            Episode episode = await episodeServices.GetEpisodeWithSeasons(model.Id);

            Guid showId = episode.Season.ShowId;
            int seasonNumber = episode.Season.SeasonNumber;

            try
            {
                episode = episodeServices.EditEpisode(model);

                await episodeServices.SaveEpisodeChanges(episode);

                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = showId, seasonNumber });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the show.");
                return View(model);
            }

        }
    }
}
