using System.ComponentModel.DataAnnotations;
using static ShowTracker.Common.ValidationConstants.ShowsValidationConstants;

namespace ShowTracker.Data.Models
{
    public class Show
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(ShowNameMinLength)]
        [MaxLength(ShowNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(ShowDescriptionMinLength)]
        [MaxLength(ShowDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public bool IsFavorite { get; set; }

        public virtual ICollection<UsersShows> Users { get; set; } = null!;
        public virtual ICollection<Season> Seasons { get; set; } = new HashSet<Season>();

    }
}
