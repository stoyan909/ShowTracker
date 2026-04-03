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

            var show = showServices.CreateShow(model);

            if (model.ShowPictureFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ShowPics");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = show.Name + show.Id + ".jpg";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (Image image = await Image.LoadAsync(model.ShowPictureFile.OpenReadStream()))
                {
                    await image.SaveAsync(filePath, new JpegEncoder());
                }
            }

            await showServices.SaveNewShow(show);

            return RedirectToAction("Index", "Explore");
        }
    }
}
