using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
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

        public List<int> SelectedGenreIds { get; set; } = new();

        public IEnumerable<SelectListItem> Genres { get; set; }
            = new List<SelectListItem>();

        [Display(Name = "Show picture")]
        public IFormFile? ShowPictureFile { get; set; }
    }
}
