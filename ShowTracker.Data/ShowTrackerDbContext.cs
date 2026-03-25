using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data.Models;

namespace ShowTracker.Data
{
    public class ShowTrackerDbContext : IdentityDbContext<IdentityUser>
    {
        public ShowTrackerDbContext(DbContextOptions<ShowTrackerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Show> Shows { get; set; } = null!;
        public DbSet<Season> Seasons { get; set; } = null!;
        public DbSet<Episode> Episodes { get; set; } = null!;
        public DbSet<UsersShows> UsersShows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersShows>()
                .HasKey(us => new { us.UserId, us.ShowId });

            modelBuilder.Entity<UsersShows>()
                .HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsersShows>()
                .HasOne(us => us.Show)
                .WithMany(s => s.Users)
                .HasForeignKey(us => us.ShowId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Show>()
                .Property(s => s.IsFavorite)
                .HasDefaultValue(false);

            modelBuilder.Entity<Season>()
                .Property(s => s.SeasonNumber)
                .HasDefaultValue(1);

            modelBuilder.Entity<Episode>()
                .Property(e=>e.IsWatched)
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowTrackerDbContext).Assembly);
        }
    }
}
