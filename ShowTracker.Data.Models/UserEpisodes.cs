using Microsoft.AspNetCore.Identity;

namespace ShowTracker.Data.Models
{
    public class UserEpisodes
    {
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public int EpisodeId { get; set; }

        public virtual Episode Episode { get; set; } = null!;
    }
}
