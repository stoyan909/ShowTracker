using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Controllers
{
    public class Show : BaseController
    {
        private readonly IShowServices showServices;
        public Show(IShowServices showServices) 
        {
            this.showServices = showServices;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync(string id)
        {
            if(showServices.IsStringNullOrEmpty(id))
            {
                return NotFound();
            }

            if (!showServices.isGuidValid(id)) 
            {
                return BadRequest();
            }

            Guid showId = showServices.GetGuidFromString(id);

            bool showExist = await showServices.ShowExistInDatabase(showId);

            if (!showExist)
            {
                return NotFound();
            }

            var show = await showServices.GetShowWithSeasonsAndEpisodes(showId);

            return View(show);
        }
    }
}
