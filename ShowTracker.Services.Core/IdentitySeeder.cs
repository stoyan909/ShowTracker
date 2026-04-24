using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ShowTracker.Data.Models;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Infrastructures
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        private readonly string[] applicationRoles = new[]
        {
            "Admin",
            "User"
        };
        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager, 
            IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task SeedAdminUserAsync()
        {
            string adminEmail = configuration["UserSeed:TestAdmin:Email"] ?? throw new InvalidOperationException("Admin email not found in configuration.");
            string adminPassword = configuration["UserSeed:TestAdmin:Password"] ?? throw new InvalidOperationException("Admin password not found in configuration.");

            ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                };

                IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("There was an error why trying to seed admin user");
                }

                bool isInRole = await userManager.IsInRoleAsync(adminUser, applicationRoles[0]);

                if (!isInRole)
                {
                    result = await userManager.AddToRoleAsync(adminUser, applicationRoles[0]);
                }

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("There was an error why trying to seed admin user");
                }
            }
        }

        public async Task SeedRolesAsync()
        {
            foreach (string role in applicationRoles)
            {
                bool rolesExist = await roleManager.RoleExistsAsync(role);

                if (!rolesExist)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>() 
                    {
                        Name = role,
                    };
                    IdentityResult identityResult = await roleManager.CreateAsync(newRole);

                    if (!identityResult.Succeeded)
                    {
                        throw new InvalidOperationException("There was an error while trying to seed role");
                    }
                }
            }
        }
    }
}
