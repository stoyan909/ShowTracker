using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Controllers
{
    public class EpisodeController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly ISeasonServices seasonServices;
        private readonly IEpisodeServices episodeServices;
        private readonly IMapper mapper;
        public EpisodeController(IShowServices showServices, IMapper mapper, ISeasonServices seasonServices, IEpisodeServices episodeServices)
        {
            this.showServices = showServices;
            this.mapper = mapper;
            this.seasonServices = seasonServices;
            this.episodeServices = episodeServices;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewEpisode(Guid id)
        {
            bool seasonExist = await seasonServices.SeasonExistInDataBase(id);

            if (!seasonExist)
            {
                return NotFound();
            }

            CreateEpisodeViewModel model = new CreateEpisodeViewModel
            {
                SeasonId = id
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

            Season? season = await seasonServices.GetSeason(model.SeasonId);

            if (season is null) 
            {
                return NotFound();
            }

            Episode episode = mapper.Map<Episode>(model);

            try
            {
                await seasonServices.AddNewEpisodeToSeasonAndSaveToDatabase(season, episode);

                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = season.ShowId, seasonNumber = season.SeasonNumber });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the season.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditEpisode(int id)
        { 
            Episode episode = await episodeServices.GetEpisodeWithSeasons(id);

            if (episode is null) 
            {
                return NotFound();
            }

            EditEpisodeViewModel model = mapper.Map<EditEpisodeViewModel>(episode);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEpisode(EditEpisodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Episode? episode = await episodeServices.GetEpisodeWithSeasons(model.Id);

            if (episode is null) 
            {
                return NotFound();
            }

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

        public async Task<IActionResult> EpisodeWatched(int id, Guid showId, int seasonNumber)
        {
            string userId = GetUserId()!;

            bool userFollowsShow = await showServices.UserShowContainsGivenShow(userId, showId);

            if (!userFollowsShow)
            {
                TempData["ErrorMessage"] = "User is not following the show and can't mark the episode as watched.";
                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = showId, seasonNumber = seasonNumber });
            }

            bool episodeExist = await episodeServices.EpisodeExistInDatabase(id);

            if (!episodeExist)
            {
                return NotFound();
            }

            bool episodeAlreadyWatched = await episodeServices.EpisodeAlreadyWatchedByUser(id, userId);

            try
            {
                await episodeServices.WatchedEpisode(id, userId, episodeAlreadyWatched);

                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = showId, seasonNumber = seasonNumber });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the show.");
                return RedirectToAction(nameof(ShowController.Index), nameof(Show), new { id = showId, seasonNumber = seasonNumber });

            }

        }
    }
}
