using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Show picture")]
        public IFormFile? ShowPictureFile { get; set; }
    }
}
