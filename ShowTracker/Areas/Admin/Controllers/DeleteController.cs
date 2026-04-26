using Microsoft.AspNetCore.Mvc;
using ShowTracker.Controllers;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class DeleteController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IEpisodeServices episodeServices;
        public DeleteController(IShowServices showServices, IEpisodeServices episodeServices)
        {
            this.showServices = showServices;
            this.episodeServices = episodeServices;
        }

        [HttpGet]
        public async Task<IActionResult> Show(Guid id)
        {
            Show? show = await showServices.GetShowWithDetails(id);

            if (show is null)
            {
                return NotFound();
            }

            DeleteShowViewModel model = new DeleteShowViewModel()
            {
                Id = show.Id,
                Title = show.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Show(Guid id, DeleteShowViewModel model)
        {
            Show? show = await showServices.GetShowWithDetails(id);

            if (show is null)
            {
                return NotFound();
            }

            showServices.DeleteShowPicture(show);

            try
            {
                await showServices.DeleteShow(show);
                return RedirectToAction(nameof(ExploreController.Index), nameof(Show));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the show.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Episode(int id)
        {
            Episode? episode = await episodeServices.GetEpisodeWithSeasons(id);

            if (episode is null)
            {
                return NotFound();
            }

            DeleteEpisodeViewModel model = new DeleteEpisodeViewModel()
            {
                Id = episode.Id,
                Title = episode.EpisodeTitle,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Episode(int id, DeleteEpisodeViewModel model)
        {
            Episode? episode = await episodeServices.GetEpisodeWithSeasons(id);

            if (episode is null)
            {
                return NotFound();
            }

            try
            {
                await episodeServices.DeleteEpisode(episode);
                return RedirectToAction(nameof(Show), nameof(Show), new { id = episode.Season.ShowId, seasonNumber = episode.Season.SeasonNumber });
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
