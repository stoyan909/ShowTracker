using Microsoft.AspNetCore.Mvc;
using ShowTracker.Services.Core.Interfaces;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Controllers
{
    public class EpisodeController : BaseController
    {
        private readonly ISeasonServices seasonServices;
        private readonly IGeneralServices generalServices;
        public EpisodeController(ISeasonServices seasonServices, IGeneralServices generalServices)
        {
            this.seasonServices = seasonServices;
            this.generalServices = generalServices;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewEpisode(string id)
        {
            if (generalServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!generalServices.isGuidValid(id))
            {
                return BadRequest();
            }

            Guid seasonId = generalServices.GetGuidFromString(id);

            bool seasonExist = await seasonServices.SeasonExistInDataBase(seasonId);

            if (!seasonExist)
            {
                return NotFound();
            }

            CreateEpisodeViewModel model = new CreateEpisodeViewModel
            {
                SeasonId = seasonId
            };

            return View(model);
        }
    }
}
