using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data;
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

        builder.Services.AddDefaultIdentity<IdentityUser>(options => ConfigureIdentity(options, builder.Configuration))
            .AddEntityFrameworkStores<ShowTrackerDbContext>();

        builder.Services.AddScoped<IExploreServices, ExploreServices>();
        builder.Services.AddScoped<IShowServices, ShowServices>();     
        builder.Services.AddScoped<IGeneralServices, GeneralServices>();

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