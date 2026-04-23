using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
using ShowTracker.Data.Models;
using ShowTracker.Infrastructures;
using ShowTracker.Mapping;
using ShowTracker.Services.Core;
using ShowTracker.Services.Core.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DevConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ShowTrackerDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options => ConfigureIdentity(options, builder.Configuration))
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ShowTrackerDbContext>();

        builder.Services.AddRazorPages();

        builder.Services.AddScoped<IExploreServices, ExploreServices>();
        builder.Services.AddScoped<IShowServices, ShowServices>();      
        builder.Services.AddScoped<ISeasonServices, SeasonServices>();
        builder.Services.AddScoped<IEpisodeServices, EpisodeServices>();
        builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(CreateShowProfile).Assembly);
            cfg.AddMaps(typeof(EditShowProfile).Assembly);
            cfg.AddMaps(typeof(EditEpisodeProfile).Assembly);
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        //Todo mahna depenci sus services
        app.UseRoleSeeder();
        app.UseAdminUserSeeder();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }

    private static void ConfigureIdentity(IdentityOptions options, ConfigurationManager configuration)
    {
        options.User.RequireUniqueEmail = configuration.GetValue<bool>("IdentityOptions:User:RequireUniqueEmail");

        options.SignIn.RequireConfirmedAccount = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedAccount");
        options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedEmail");
        options.SignIn.RequireConfirmedPhoneNumber = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedPhoneNumber");

        options.Password.RequireDigit = configuration.GetValue<bool>("IdentityOptions:Password:RequireDigit");
        options.Password.RequireLowercase = configuration.GetValue<bool>("IdentityOptions:Password:RequireLowercase");
        options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("IdentityOptions:Password:RequireNonAlphanumeric");
        options.Password.RequireUppercase = configuration.GetValue<bool>("IdentityOptions:Password:RequireUppercase");
        options.Password.RequiredLength = configuration.GetValue<int>("IdentityOptions:Password:RequiredLength");
    }
}