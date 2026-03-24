using Microsoft.AspNetCore.Identity;

namespace ShowTracker.Data.Models
{
    public class UsersShows
    {
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public Guid ShowId { get; set; }

        public virtual Show Show { get; set; } = null!;
    }
}
