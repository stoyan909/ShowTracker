using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShowTracker.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
