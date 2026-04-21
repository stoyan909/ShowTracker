using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShowTracker.Data.Models;

namespace ShowTracker.Data
{
    public class ShowTrackerDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ShowTrackerDbContext(DbContextOptions<ShowTrackerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Show> Shows { get; set; } = null!;
        public DbSet<Season> Seasons { get; set; } = null!;
        public DbSet<Episode> Episodes { get; set; } = null!;
        public DbSet<UsersShows> UsersShows { get; set; } = null!;
        public DbSet<UserEpisodes> UsersEpisodes { get; set; } = null!;

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

            modelBuilder.Entity<UserEpisodes>()
                .HasKey(ue => new { ue.UserId, ue.EpisodeId });

            modelBuilder.Entity<UserEpisodes>()
                .HasOne(ue => ue.User)
                .WithMany()
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEpisodes>()
                .HasOne(ue => ue.Episode)
                .WithMany(e => e.Users)
                .HasForeignKey(ue => ue.EpisodeId)
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
