using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTracker.Data.Models;

namespace ShowTracker.Data.Configurations
{
    internal class ShowGenreEntityTypeConfiguration : IEntityTypeConfiguration<ShowGenres>
    {
        public void Configure(EntityTypeBuilder<ShowGenres> builder)
        {
            builder.HasData
                (
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9"),
                        GenreId = 4
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9"),
                        GenreId = 3
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"),
                        GenreId = 3
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"),
                        GenreId = 2
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"),
                        GenreId = 6
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01"),
                        GenreId = 1
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01"),
                        GenreId = 5
                    },
                    new ShowGenres
                    {
                        ShowId = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01"),
                        GenreId = 7
                    }
                );
        }
    }
}
