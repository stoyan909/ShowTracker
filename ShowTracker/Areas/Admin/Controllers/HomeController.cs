using Microsoft.AspNetCore.Mvc;

namespace ShowTracker.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
