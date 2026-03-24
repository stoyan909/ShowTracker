using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ShowTracker.Common.ValidationConstants.EpisodesValidationConstants;

namespace ShowTracker.Data.Models
{
    public class Episode
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(EpisodeTitleMinLength)]
        [MaxLength(EpisodeTitleMaxLength)]
        public string EpisodeTitle { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public bool IsWatched { get; set; }

        [Required]
        [ForeignKey(nameof(Season))]
        public int SeasonId { get; set; }

        public virtual Season Season { get; set; } = null!;

        public string? ImageUrl { get; set; }

    }
}
