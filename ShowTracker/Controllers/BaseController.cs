using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShowTracker.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            return Guid.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
