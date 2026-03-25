using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTracker.Data.Models;

namespace ShowTracker.Data.Configurations
{
    public class ShowEntityTypeConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.HasData(
                new Show
                {
                    Id = Guid.Parse("00dcb3bf-6ea7-47d4-bfb9-edd6f48c94a9"),
                    Name = "Chernobyl",
                    Description = "In April 1986, the city of Chernobyl in the Soviet Union suffers one of the worst nuclear disasters in the history of mankind. Consequently, many heroes put their lives on the line in the following days, weeks and months."
                },
                new Show
                {
                    Id = Guid.Parse("d1c9e5b8-7a0c-4f1e-9b3a-2c8e5f6a7b8c"),
                    Name = "Wednesday",
                    Description = "Follows Wednesday Addams' years as a student, when she attempts to master her emerging psychic ability, thwart a killing spree, and solve the mystery that embroiled her parents."
                },
                new Show
                {
                    Id = Guid.Parse("a2b3c4d5-e6f7-8901-2345-6789abcdef01"),
                    Name = "Hell's Paradise",
                    Description = "A squad of prisoners and their guards are sent to investigate a mysterious island. They get stranded there and must rely on each other to survive the island's mysterious and monstrous residents."
                });
        }
    }
}
