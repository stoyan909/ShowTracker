using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class EpisodeController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IEpisodeServices episodeServices;
        public EpisodeController(IShowServices showServices, IEpisodeServices episodeServices)
        {
            this.showServices = showServices;
            this.episodeServices = episodeServices;
        }

        public async Task<IActionResult> EpisodeWatched(int id, Guid showId, int seasonNumber)
        {
            Guid userId = GetUserId()!;

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
