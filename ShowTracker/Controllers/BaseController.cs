using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShowTracker.Controllers
{
    //[Authorize]
    public abstract class BaseController : Controller
    {
        protected string? GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
