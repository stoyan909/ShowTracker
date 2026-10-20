using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShowTracker.Data.Models;
using System.ComponentModel.DataAnnotations;
using static ShowTracker.Common.ValidationConstants.ShowsValidationConstants;

namespace ShowTracker.ViewModel.ShowsViewModel
{
    public class EditShowViewModel
    {
        public Guid Id { get; set; }

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
        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        public List<ShowGenres> Genres { get; set; } = new List<ShowGenres>();

        [Required]
        [Display(Name = "Genres")]
        public List<int> SelectedGenres { get; set; } = new List<int>();

        [Display(Name = "Show picture")]
        public IFormFile? ShowPictureFile { get; set; }
    }
}
