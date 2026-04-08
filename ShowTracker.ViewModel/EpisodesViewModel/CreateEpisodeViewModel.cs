using System.ComponentModel.DataAnnotations;
using static ShowTracker.Common.ValidationConstants.EpisodesValidationConstants;

namespace ShowTracker.ViewModel.EpisodesViewModel
{
    public class CreateEpisodeViewModel
    {
        [Required]
        [MinLength(EpisodeTitleMinLength)]
        [MaxLength(EpisodeTitleMaxLength)]
        [Display(Name = "Episode title")]
        public string EpisodeTitle { get; set; } = null!;

        [Required]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        public Guid SeasonId { get; set; }

        [Display(Name = "Episode image URL")]
        public string? ImageUrl { get; set; }
    }
}
