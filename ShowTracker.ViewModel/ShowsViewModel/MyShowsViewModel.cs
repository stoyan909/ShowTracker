namespace ShowTracker.ViewModel.ShowsViewModel
{
    public class MyShowsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int TotalEpisodes { get; set; }

        public DateTime? FollowedOn { get; set; }

    }
}
