using System.ComponentModel.DataAnnotations;

namespace ShowTracker.ViewModel.EpisodesViewModel
{
    public class DeleteEpisodeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Show title")]
        public string Title { get; set; } = null!;
    }
}
