using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ShowTracker.Common.ValidationConstants.EpisodesValidationConstants;
using ShowTracker.Data.Models;

namespace ShowTracker.ViewModel.EpisodesViewModel
{
    public class EditEpisodeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(EpisodeTitleMinLength)]
        [MaxLength(EpisodeTitleMaxLength)]
        [Display(Name = "Title")]
        public string EpisodeTitle { get; set; } = null!;

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; } = DateTime.Today;

        [Display(Name = "Episode image URL")]
        public string? ImageUrl { get; set; }

        public Guid SeasonId { get; set; }

        public Season? Season { get; set; }
    }
}
