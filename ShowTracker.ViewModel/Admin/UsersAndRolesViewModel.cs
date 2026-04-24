namespace ShowTracker.ViewModel.Admin
{
    public class UsersAndRolesViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = new List<string>();

    }
}
