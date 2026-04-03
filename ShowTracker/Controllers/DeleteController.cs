using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Controllers
{
    public class DeleteController : Controller
    {
        private readonly IShowServices showServices;
        private readonly IGeneralServices generalServices;
        public DeleteController(IShowServices showServices, IGeneralServices generalServices)
        {
            this.showServices = showServices;
            this.generalServices = generalServices;
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

            string filePath = $"wwwroot/ShowPics/{show.Name + show.Id}.jpg";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            try
            {
                await showServices.DeleteShow(show);
                return RedirectToAction("Index", "Explore");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the show.");
                return View(model);
            }


        }
    }
}
