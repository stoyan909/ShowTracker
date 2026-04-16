using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Common;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Controllers
{
    public class ShowController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IMapper mapper;
        public ShowController(IShowServices showServices, IMapper mapper)
        {
            this.showServices = showServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id, int seasonNumber)
        {
            Show? show = await showServices.GetShowWithDetails(id);

            if (show == null) 
            {
                return NotFound();
            }

            if (seasonNumber < 1) 
            {
                return BadRequest();
            }

            ViewBag.SeasonNumber = seasonNumber;

            return View(show);
        }

        [HttpGet]
        public async Task<IActionResult> FollowShow(Guid id)
        {
            bool showExist = await showServices.ShowExistInDatabase(id);

            if (!showExist)
            {
                return NotFound();
            }

            string userId = GetUserId()!;

            await showServices.ToggleFollowAsync(id, userId);

            string returnUrl = Request.Headers["Referer"].ToString();

            if (string.IsNullOrWhiteSpace(returnUrl)) 
            {
                return RedirectToAction(nameof(ExploreController.Index),"Explore");
            }

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

            Show show = mapper.Map<Show>(model);

            show = showServices.AddMultipleSeasonToShow(show, model.SeasonNumber);

            await showServices.GeneratePictureForShow(model.ShowPictureFile, show.Name, show.Id.ToString());

            await showServices.SaveNewShow(show);

            return RedirectToAction(nameof(ExploreController.Index), "Explore");
        }

        [HttpGet]
        public async Task<IActionResult> EditShow(Guid id )
        {
            Show? show = await showServices.GetShowWithDetails(id);

            if (show is null) 
            {
                return NotFound();
            }

            EditShowViewModel model = mapper.Map<EditShowViewModel>(show);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditShow(EditShowViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Show show = await showServices.GetShowWithDetails(model.Id);

            if (show is null) 
            {
                return NotFound();
            }

            if (model.ShowPictureFile != null || show.Name != model.Name) 
            {
                showServices.DeleteShowPicture(show);

                await showServices.GeneratePictureForShow(model.ShowPictureFile, model.Name, model.Id.ToString());
            }

            int count = HelperMethods.GetSeasonDifference(show.Seasons.Count(), model.SeasonNumber);

            if (count > 0) 
            {
                return RedirectToAction(nameof(SeasonController.CreateSeason), "Season", new { id = show.Id, count = count});  
            }
            else if (count < 0)
            {
                return RedirectToAction(nameof(SeasonController.DeleteSeason), "Season", new { id = show.Id, count = -count});
            }

            mapper.Map(model, show);

            try
            {
                await showServices.SaveEditShow(show);

                return RedirectToAction(nameof(ShowController.Index), new { id = show.Id, seasonNumber = 1 });
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
