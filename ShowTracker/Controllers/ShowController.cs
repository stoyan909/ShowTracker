using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ShowTracker.Controllers
{
    public class ShowController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IGeneralServices generalServices;
        public ShowController(IShowServices showServices, IGeneralServices generalServices)
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

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(showId);

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

        [HttpGet]
        public IActionResult CreateShow()
        {
            CreateShowViewModel model = new CreateShowViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShow(CreateShowViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool showExist = await showServices.ShowExistInDatabase(model.Name);

            if (showExist)
            {
                ModelState.Clear();
                ModelState.AddModelError("Name", "This show already exists");
                return View("CreateShow", new CreateShowViewModel());
            }

            Show show = showServices.CreateShow(model);

            await showServices.GeneratePictureForShow(model.ShowPictureFile, show.Name, show.Id.ToString());

            await showServices.SaveNewShow(show);

            return RedirectToAction("Index", "Explore");
        }

        [HttpGet]
        public async Task<IActionResult> EditShow(string id )
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

            EditShowViewModel model = new EditShowViewModel()
            {
                Id = show.Id,
                Name = show.Name,
                Description = show.Description,
                SeasonNumber = show.Seasons.Count
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditShow(EditShowViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool showExist = await showServices.ShowExistInDatabase(model.Id);

            if (!showExist)
            {
                return NotFound();
            }

            Show show = await showServices.GetShowWithSeasonsAndEpisodes(model.Id);

            if (model.ShowPictureFile != null) 
            {
                showServices.DeleteShowPicture(show);

                await showServices.GeneratePictureForShow(model.ShowPictureFile, model.Name, model.Id.ToString());
            }

            if (model.SeasonNumber > show.Seasons.Count) 
            {
                // If the new season number is greater than the current number of seasons, we need to add new seasons to the show
            }

            try
            {
                show = showServices.EditShow(model);

                await showServices.SaveEditShow(show);

                return RedirectToAction("Index", new { id = show.Id, seasonNumber = 1 });
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
