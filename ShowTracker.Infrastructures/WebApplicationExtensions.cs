using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Infrastructures
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseRoleSeeder(this IApplicationBuilder applicationBuilder)
        {
            using IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope();

            IIdentitySeeder identitySeeder = scope.ServiceProvider.GetRequiredService<IIdentitySeeder>();

            identitySeeder.SeedRolesAsync()
                .GetAwaiter()
                .GetResult();

            return applicationBuilder;
        }

        public static IApplicationBuilder UseAdminUserSeeder(this IApplicationBuilder applicationBuilder) 
        {
            using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            IIdentitySeeder identitySeeder = serviceScope.ServiceProvider.GetRequiredService<IIdentitySeeder>();

            identitySeeder.SeedAdminUserAsync().GetAwaiter().GetResult();

            return applicationBuilder;
        }
    }
}
