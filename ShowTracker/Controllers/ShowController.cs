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

            Guid userId = GetUserId()!;

            await showServices.ToggleFollowAsync(id, userId);

            string returnUrl = Request.Headers["Referer"].ToString();

            if (string.IsNullOrWhiteSpace(returnUrl)) 
            {
                return RedirectToAction(nameof(ExploreController.Index),"Explore");
            }

            return Redirect(returnUrl);
        }
    }
}
