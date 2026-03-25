using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using ShowTracker.Data.Models;

namespace ShowTracker.Data.Configurations
{
    public class EpisodeEntityTypeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            int id = 1;

            Guid chernobyGuid = Guid.Parse("c9d46740-7c4b-4380-8122-adee43c370e8");

            DateTime[] chernobylEpisodesReleseDates = [
                new DateTime(2019, 5, 7),
                new DateTime(2019, 5, 14),
                new DateTime(2019, 5, 21),
                new DateTime(2019, 5, 28),
                new DateTime(2019, 6, 4)];

            string[] chernobylEpisodesTitles = [
                "1:23:45",
                "Please Remain Calm",
                "Open Wide, O Earth",
                "The Happiness of All Mankind",
                "Vichnaya Pamyat"];

            string[] chernobylEpisodeImageUrls = [
                "https://image.tmdb.org/t/p/w500/hlLXt2tOPT6RRnjiUmoxyG1LTFi.jpg",
                "https://image.tmdb.org/t/p/w500/8jv8c8QnXl16rDpD6PEw24kTx5c.jpg",
                "https://image.tmdb.org/t/p/w500/6uBlEXw6p7uXJ9VjA36TObgGJE0.jpg",
                "https://image.tmdb.org/t/p/w500/jyqG0vUj8kT8y9JteLoI0lpBT1s.jpg",
                "https://image.tmdb.org/t/p/w500/4rWQK9r0p5momkGumZ5qX6Ch12H.jpg"];

            List<Episode> chenobylEpisodes = new List<Episode>();

            chenobylEpisodes = EpisodesOfSeason(id, chernobylEpisodesTitles, chernobylEpisodesReleseDates, chernobyGuid, chernobylEpisodeImageUrls);

            id = IdGenerator(id, chenobylEpisodes);

            Guid wednesdaySeasonOneGuid = Guid.Parse("0f390bd4-7a7d-4ed2-92a7-6db49306f808");

            DateTime[] wednesdayEpisodesReleseDates = [
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23),
                new DateTime(2022, 11, 23)];

            string[] wednesdayEpisodesTitles = [
                "Wednesday's Child Is Full of Woe",
                "Woe Is the Loneliest Number",
                "Friend or Woe",
                "Woe What a Night",
                "You Reap What You Woe",
                "Quid Pro Woe",
                "If You Don't Woe Me by Now",
                "A Murder of Woes"];

            string[] wednesdayEpisodeImageUrls = [
                "https://image.tmdb.org/t/p/w500/9PFonBhy4cQy7Jz20NpMygczOkv.jpg",
                "https://image.tmdb.org/t/p/w500/eYV6G6c5wPLXnw0lPwQBGzb62LF.jpg",
                "https://image.tmdb.org/t/p/w500/w0Fao9u1Utn8Hj6kUMlzUxr87hc.jpg",
                "https://image.tmdb.org/t/p/w500/6z9XqH0I1GtIsfRSAxEPPYUajF2.jpg",
                "https://image.tmdb.org/t/p/w500/3sX5Yucs5cjox96D65gis6pZeRA.jpg",
                "https://image.tmdb.org/t/p/w500/pF3vCtbmZh9rqx8uxFazc2DSES0.jpg",
                "https://image.tmdb.org/t/p/w500/jPaz6RtWLpmjHtkobaN6D2PfYZ7.jpg",
                "https://image.tmdb.org/t/p/w500/n8I2c2MvmP0xSg6u40qCMgfHdCq.jpg"];

            List<Episode> wednesdaySeasonOneEpisodes = new List<Episode>();

            wednesdaySeasonOneEpisodes = EpisodesOfSeason(id, wednesdayEpisodesTitles, wednesdayEpisodesReleseDates, wednesdaySeasonOneGuid, wednesdayEpisodeImageUrls);

            id = IdGenerator(id, wednesdaySeasonOneEpisodes);

            Guid wednesdaySeasonTwoGuid = Guid.Parse("9c524886-a88e-49b3-a510-025b76f4cf27");

            wednesdayEpisodesReleseDates = [
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),
                new DateTime(2025, 8, 6),];

            wednesdayEpisodesTitles = [
                "Here We Woe Again",
                "The Devil You Woe",
                "Call of the Woe",
                "If These Woes Could Talk",
                "Hyde and Woe Seek",
                "Woe Thyself",
                "Woe Me The Money",
                "This Means Woe"];

            wednesdayEpisodeImageUrls = [
                "https://image.tmdb.org/t/p/w500/1.jpg",
                "https://image.tmdb.org/t/p/w500/2.jpg",
                "https://image.tmdb.org/t/p/w500/3.jpg",
                "https://image.tmdb.org/t/p/w500/4.jpg",
                "https://image.tmdb.org/t/p/w500/5.jpg",
                "https://image.tmdb.org/t/p/w500/6.jpg",
                "https://image.tmdb.org/t/p/w500/7.jpg",
                "https://image.tmdb.org/t/p/w500/8.jpg"];

            List<Episode> wednesdaySeasonTwoEpisodes = new List<Episode>();

            wednesdaySeasonTwoEpisodes = EpisodesOfSeason(id, wednesdayEpisodesTitles, wednesdayEpisodesReleseDates, wednesdaySeasonTwoGuid, wednesdayEpisodeImageUrls);

            id = IdGenerator(id, wednesdaySeasonTwoEpisodes);

            Guid hellsPradiseSeasonOneGuid = Guid.Parse("64b81beb-e7dc-409e-a004-0cd2d0442cde");

            DateTime[] hellsParadiseReleaseDates = [
                new DateTime(2023, 4, 1),
                new DateTime(2023, 4, 8),
                new DateTime(2023, 4, 15),
                new DateTime(2023, 4, 22),
                new DateTime(2023, 4, 29),
                new DateTime(2023, 5, 6),
                new DateTime(2023, 5, 13),
                new DateTime(2023, 5, 20),
                new DateTime(2023, 6, 3),
                new DateTime(2023, 6, 10),
                new DateTime(2023, 6, 17),
                new DateTime(2023, 6, 24),
                new DateTime(2023, 7, 1)];

            string[] hellsParadiseTitles = [
                "The Death Row Convict and the Executioner",
                "Screening and Choosing",
                "Weakness and Strength",
                "Hell and Paradise",
                "The Samurai and the Woman",
                "Heart and Reason",
                "Flowers and Offerings",
                "Student and Master",
                "Gods and People",
                "Yin and Yang",
                "Weak and Strong",
                "Umbrella and Ink",
                "Dreams and Reality"];

            string[] hellsParadiseEpisodeImageUrls = [
                "https://image.tmdb.org/t/p/w500/1XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z1.jpg",
                "https://image.tmdb.org/t/p/w500/2XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z2.jpg",
                "https://image.tmdb.org/t/p/w500/3XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z3.jpg",
                "https://image.tmdb.org/t/p/w500/4XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z4.jpg",
                "https://image.tmdb.org/t/p/w500/5XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z5.jpg",
                "https://image.tmdb.org/t/p/w500/6XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z6.jpg",
                "https://image.tmdb.org/t/p/w500/7XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z7.jpg",
                "https://image.tmdb.org/t/p/w500/8XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z8.jpg",
                "https://image.tmdb.org/t/p/w500/9XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z9.jpg",
                "https://image.tmdb.org/t/p/w500/10XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z10.jpg",
                "https://image.tmdb.org/t/p/w500/11XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z11.jpg",
                "https://image.tmdb.org/t/p/w500/12XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z12.jpg",
                "https://image.tmdb.org/t/p/w500/13XhC3j9w2k6Hf8c7Q3z7Zz0Z0Z13.jpg"];

            List<Episode> hellsParadiseSeasonOneEpisodes = new List<Episode>();

            hellsParadiseSeasonOneEpisodes = EpisodesOfSeason(id, hellsParadiseTitles, hellsParadiseReleaseDates, hellsPradiseSeasonOneGuid, hellsParadiseEpisodeImageUrls);

            id = IdGenerator(id, hellsParadiseSeasonOneEpisodes);

            Guid hellsParadiseSeasonTwoGuid = Guid.Parse("87932afe-1b94-46e6-83a3-034b0aa183be");

            hellsParadiseReleaseDates = [
                new DateTime(2026, 1, 11),
                new DateTime(2026, 1, 18),
                new DateTime(2026, 1, 25),
                new DateTime(2026, 2, 1),
                new DateTime(2026, 2, 8),
                new DateTime(2026, 2, 15),
                new DateTime(2026, 2, 22),
                new DateTime(2026, 3, 1),
                new DateTime(2026, 3, 8),
                new DateTime(2026, 3, 15),
                new DateTime(2026, 3, 22),
                new DateTime(2026, 3, 29)];

            hellsParadiseTitles = [
                "Dawn and Delirium",
                "Reality and Illusion",
                "Immutability and Change",
                "The Samurai Code and Carnage",
                "Humans and Sages",
                "Hindering and Restoration",
                "Two People and One Person",
                "Chrysanthemum and Peach",
                "Love and Karma",
                "Master and Disciple",
                "Ephemeralness and Fire",
                "Episode #2.12"];

            hellsParadiseEpisodeImageUrls = [
                "https://image.tmdb.org/t/p/w500/a1.jpg",
                "https://image.tmdb.org/t/p/w500/a2.jpg",
                "https://image.tmdb.org/t/p/w500/a3.jpg",
                "https://image.tmdb.org/t/p/w500/a4.jpg",
                "https://image.tmdb.org/t/p/w500/a5.jpg",
                "https://image.tmdb.org/t/p/w500/a6.jpg",
                "https://image.tmdb.org/t/p/w500/a7.jpg",
                "https://image.tmdb.org/t/p/w500/a8.jpg",
                "https://image.tmdb.org/t/p/w500/a9.jpg",
                "https://image.tmdb.org/t/p/w500/a10.jpg",
                "https://image.tmdb.org/t/p/w500/a11.jpg",
                "https://image.tmdb.org/t/p/w500/a12.jpg"];

            List<Episode> hellsParadiseSeasonTwoEpisodes = new List<Episode>();

            hellsParadiseSeasonTwoEpisodes = EpisodesOfSeason(id, hellsParadiseTitles, hellsParadiseReleaseDates, hellsParadiseSeasonTwoGuid, hellsParadiseEpisodeImageUrls);

            builder.HasData(chenobylEpisodes);
            builder.HasData(wednesdaySeasonOneEpisodes);
            builder.HasData(wednesdaySeasonTwoEpisodes);
            builder.HasData(hellsParadiseSeasonOneEpisodes);
            builder.HasData(hellsParadiseSeasonTwoEpisodes);
        }

        public List<Episode> EpisodesOfSeason(int id, string[] titles, DateTime[] releaseDates, Guid seasonId,string[]? imageUrls)
        {
            List<Episode> episodes = new List<Episode>();

            for (int i = 0; i < titles.Length; i++)
            {
                episodes.Add(new Episode
                {
                    Id = id++,
                    EpisodeTitle = titles[i],
                    ReleaseDate = releaseDates[i],
                    ImageUrl = imageUrls[i] != null ? imageUrls[i] : null,
                    SeasonId = seasonId
                });


            }

            return episodes;
        }

        public int IdGenerator(int id, List<Episode> countOfEpisodes)
        {
            int newStartId = id + countOfEpisodes.Count();
            return newStartId;
        }
    }
}
