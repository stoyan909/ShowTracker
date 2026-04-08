using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Controllers
{
    public class DeleteController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IGeneralServices generalServices;
        private readonly IEpisodeServices episodeServices;
        public DeleteController(IShowServices showServices, IGeneralServices generalServices, IEpisodeServices episodeServices)
        {
            this.showServices = showServices;
            this.generalServices = generalServices;
            this.episodeServices = episodeServices;
        }

        [HttpGet]
        public async Task<IActionResult> Show(string id)
        {
            bool isStringNullOrEmpty = generalServices.IsStringNullOrEmpty(id);

            if (isStringNullOrEmpty)
            {
                return NotFound();
            }

            bool isGuidValid = generalServices.isGuidValid(id);

            if (!isGuidValid)
            {
                return BadRequest();
            }

            Guid showGuidId = generalServices.GetGuidFromString(id);

            bool showExist = await showServices.ShowExistInDatabase(showGuidId);

            if (!showExist)
            {
                return NotFound();
            }

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(showGuidId);

            DeleteShowViewModel model = new DeleteShowViewModel()
            {
                Id = show.Id,
                Title = show.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Show(string id, DeleteShowViewModel model)
        {
            bool isStringNullOrEmpty = generalServices.IsStringNullOrEmpty(id);

            if (isStringNullOrEmpty)
            {
                return NotFound();
            }

            bool isGuidValid = generalServices.isGuidValid(id);

            if (!isGuidValid)
            {
                return BadRequest();
            }

            Guid showGuidId = generalServices.GetGuidFromString(id);

            bool showExist = await showServices.ShowExistInDatabase(showGuidId);

            if (!showExist)
            {
                return NotFound();
            }

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(showGuidId);

            showServices.DeleteShowPicture(show);

            try
            {
                await showServices.DeleteShow(show);
                return RedirectToAction(nameof(ExploreController.Index), "Explore");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the show.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Episode(string id)
        {
            bool isStringNullOrEmpty = generalServices.IsStringNullOrEmpty(id);

            if (isStringNullOrEmpty)
            {
                return NotFound();
            }

            bool isIntValid = generalServices.isIntValid(id);

            if (!isIntValid)
            {
                return BadRequest();
            }

            int episodeId = generalServices.GetIntFromString(id);

            bool episodeExist = await episodeServices.EpisodeExistInDatabase(episodeId);

            if (!episodeExist)
            {
                return NotFound();
            }

            Episode episode = await episodeServices.GetEpisode(episodeId);

            DeleteEpisodeViewModel model = new DeleteEpisodeViewModel()
            {
                Id = episode.Id,
                Title = episode.EpisodeTitle,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Episode(string id, DeleteEpisodeViewModel model)
        {
            bool isStringNullOrEmpty = generalServices.IsStringNullOrEmpty(id);

            if (isStringNullOrEmpty)
            {
                return NotFound();
            }

            bool isIntValid = generalServices.isIntValid(id);

            if (!isIntValid)
            {
                return BadRequest();
            }

            int episodeIntId = generalServices.GetIntFromString(id);

            bool episodeExist = await episodeServices.EpisodeExistInDatabase(episodeIntId);

            if (!episodeExist)
            {
                return NotFound();
            }

            Episode episode = await episodeServices.GetEpisode(episodeIntId);

            try
            {
                await episodeServices.DeleteEpisode(episode);
                return RedirectToAction(nameof(ExploreController.Index), "Explore");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the episode.");
                return View(model);
            }
        }
    }
}
