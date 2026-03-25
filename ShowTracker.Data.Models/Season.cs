using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowTracker.Data.Models
{
    public class Season
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int SeasonNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Show))]
        public Guid ShowId { get; set; }
        public virtual Show Show { get; set; } = null!;

        public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();
    }
}
