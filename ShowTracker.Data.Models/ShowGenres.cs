namespace ShowTracker.Data.Models
{
    public class ShowGenres
    {
        public Guid ShowId { get; set; }

        public virtual Show Show { get; set; } = null!;

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
    }
}
