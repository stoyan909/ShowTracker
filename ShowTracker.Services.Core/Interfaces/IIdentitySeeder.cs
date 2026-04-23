namespace ShowTracker.Services.Core.Interfaces
{
    public interface IIdentitySeeder
    {
        Task SeedRolesAsync();

        Task SeedAdminUserAsync();
    }
}
