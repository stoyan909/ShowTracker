using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTracker.Data.Models;

namespace ShowTracker.Data.Configurations
{
    public class SeasonEntityTypeConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasData
                (
                    new Season
                    {
                        Id = Guid.Parse("c9d46740-7c4b-4380-8122-adee43c370e8"),
                        SeasonNumber = 1,
                        ShowId = Guid.Parse("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9") //Chernobyl
                    },
                    new Season
                    {
                        Id = Guid.Parse("0f390bd4-7a7d-4ed2-92a7-6db49306f808"),
                        SeasonNumber = 1,
                        ShowId = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") //Wednesday
                    },
                    new Season
                    {
                        Id = Guid.Parse("9c524886-a88e-49b3-a510-025b76f4cf27"),
                        SeasonNumber = 2,
                        ShowId = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c") //Wednesday
                    },
                    new Season
                    {
                        Id = Guid.Parse("64b81beb-e7dc-409e-a004-0cd2d0442cde"),
                        SeasonNumber = 1,
                        ShowId = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01") //Hell's Paradise
                    },
                    new Season
                    {
                        Id = Guid.Parse("87932afe-1b94-46e6-83a3-034b0aa183be"),
                        SeasonNumber = 2,
                        ShowId = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01")//Hell's Paradise
                    }
                );
        }
    }
}
