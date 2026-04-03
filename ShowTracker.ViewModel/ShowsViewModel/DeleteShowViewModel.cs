using System.ComponentModel.DataAnnotations;

namespace ShowTracker.ViewModel.ShowsViewModel
{
    public class DeleteShowViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Show title")]
        public string Title { get; set; } = null!;
    }
}
