using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class Show : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IGeneralServices generalServices;
        public Show(IShowServices showServices, IGeneralServices generalServices)
        {
            this.showServices = showServices;
            this.generalServices = generalServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id, int seasonNumber)
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

            var show = await showServices.GetShowWithSeasonsAndEpisodes(showId);

            ViewBag.SeasonNumber = seasonNumber;

            return View(show);
        }

        public async Task<IActionResult> FollowShow(string id)
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
            string userId = GetUserId()!;

            bool userFollowsGivenShow = await showServices.UserShowContainsGivenShow(userId, showId);

            if (!userFollowsGivenShow)
            {
                UsersShows usersShows = showServices.FollowShow(userId, showId);
                await showServices.SaveNewUserShowToDataBase(usersShows);
            }
            else 
            {
                await showServices.UnfollowShow(userId, showId);
            }

            string returnUrl = Request.Headers["Referer"].ToString();

            return Redirect(returnUrl);
        }
    }
}
