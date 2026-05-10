using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Common;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class ShowController : BaseController
    {
        private readonly IShowServices showServices;
        private readonly IMapper mapper;
        private readonly IAdminServices adminServices;
        public ShowController(IShowServices showServices, IMapper mapper, IAdminServices adminServices)
        {
            this.showServices = showServices;
            this.mapper = mapper;
            this.adminServices = adminServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await showServices.GetAllShowWithDetailsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Show(Guid id, int seasonNumber)
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
                return View(nameof(CreateShow), new CreateShowViewModel());
            }

            Show show = mapper.Map<Show>(model);

            show = adminServices.AddMultipleSeasonToShow(show, model.SeasonNumber, showExist);

            await adminServices.GeneratePictureForShow(model.ShowPictureFile, show.Name, show.Id.ToString());

            await adminServices.SaveNewShow(show);

            return RedirectToAction(nameof(Show), nameof(Show), new { id = show.Id, seasonNumber = 1});
        }

        [HttpGet]
        public async Task<IActionResult> EditShow(Guid id)
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

            Show? show = await showServices.GetShowWithDetails(model.Id);

            if (show is null)
            {
                return NotFound();
            }

            if (model.ShowPictureFile != null || show.Name != model.Name)
            {
                adminServices.DeleteShowPicture(show);

                await adminServices.GeneratePictureForShow(model.ShowPictureFile, model.Name, model.Id.ToString());
            }

            int count = HelperMethods.GetSeasonDifference(show.Seasons.Count(), model.SeasonNumber);

            if (count > 0)
            {
                return RedirectToAction(nameof(SeasonController.CreateSeason), nameof(Season), new { id = show.Id, count = count});
            }
            else if (count < 0)
            {
                return RedirectToAction(nameof(SeasonController.DeleteSeason),  nameof(Season), new { id = show.Id, count = -count});
            }

            mapper.Map(model, show);

            try
            {
                await adminServices.SaveEditShow(show);

                return RedirectToAction(nameof(Show), new { id = show.Id, seasonNumber = 1 });
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
