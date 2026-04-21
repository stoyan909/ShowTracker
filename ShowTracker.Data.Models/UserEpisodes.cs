namespace ShowTracker.Data.Models
{
    public class UserEpisodes
    {
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public int EpisodeId { get; set; }

        public virtual Episode Episode { get; set; } = null!;
    }
}
