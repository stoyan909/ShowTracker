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
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Guid.Empty;
            }
            else 
            {
                return Guid.Parse(userId);
            }

        }
    }
}
