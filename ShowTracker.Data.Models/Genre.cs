using System.ComponentModel.DataAnnotations;

namespace ShowTracker.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<ShowGenres> Shows { get; set; } = new HashSet<ShowGenres>();
    }
}
