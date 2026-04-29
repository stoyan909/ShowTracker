namespace ShowTracker.Data.Models
{
    public class UsersShows
    {
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public Guid ShowId { get; set; }

        public virtual Show Show { get; set; } = null!;

        public DateTime? FollowedDate { get; set; }
    }
}
