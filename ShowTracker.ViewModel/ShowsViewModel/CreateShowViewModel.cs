using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShowTracker.Data.Models;
using static ShowTracker.Common.ValidationConstants.ShowsValidationConstants;

namespace ShowTracker.ViewModel.ShowsViewModel
{
    public class CreateShowViewModel
    {
        [Required]
        [Display(Name = "Show title")]
        [MinLength(ShowNameMinLength)]
        [MaxLength(ShowNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Show description")]
        [MinLength(ShowDescriptionMinLength)]
        [MaxLength(ShowDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Seasons")]
        public int SeasonNumber { get; set; } = 1;

        public List<ShowGenres> Genres { get; set; } = new List<ShowGenres>();

        [Required]
        [Display(Name = "Genres")]
        public List<int>SelectedGenres { get; set; } = new List<int>();

        [Display(Name = "Show picture")]
        public IFormFile? ShowPictureFile { get; set; }
    }
}
