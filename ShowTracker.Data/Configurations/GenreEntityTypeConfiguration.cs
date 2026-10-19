using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTracker.Data.Models;

namespace ShowTracker.Data.Configurations
{
    internal class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData
                (
                    new Genre 
                    {
                        Id = 1,
                        Name = "Action"
                    },
                    new Genre 
                    {
                        Id = 2,
                        Name = "Mystery"
                    },
                    new Genre 
                    {
                        Id = 3,
                        Name = "Thriller"
                    },
                    new Genre 
                    {
                        Id = 4,
                        Name = "History"
                    },
                    new Genre 
                    {
                        Id = 5,
                        Name = "Adventure"
                    },
                    new Genre 
                    {
                        Id = 6,
                        Name = "Horror"
                    },
                    new Genre 
                    {
                        Id = 7,
                        Name = "Survival"
                    }
                );
        }
    }
}
