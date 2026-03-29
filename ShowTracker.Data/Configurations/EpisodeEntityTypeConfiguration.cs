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
                "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/21acd328-6298-4928-bb08-e926d776f63d/a53d81424437914d76ce305e1f1c3634b5736dc9.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320",
                "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/7c3b4584-c2b1-482f-bc8c-18c91bf1acf1/87eced8d07e3275e259e8e4325e43abac7cc350d.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320",
                "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/e8d738e3-7bd3-40e5-be1a-d6b2c270064c/f9c7ffb5a19c43ade5b4495cc9ac8613a0831315.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320",
                "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/7cc1a700-bd77-456c-9d61-aa95cff7d73b/072b882e161aafb797c01c92f2e3a19b66882e1d.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320",
                "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/8d42e46a-0b6d-4acd-85f8-d980ec41769c/0df0fdbb626c582ac918d64a5fb0234a890448dd.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=320"];

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
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRau0OfdR2XiDVJKbosrUguN9E823JJik83QPSbk78xpYSlsuA3ukuo9v2aWKPIHmFCrvcl0kU7jy0mT_FGp1DNg1O0lzZ3VPTrgPhtT4WB2_slAE1osX6d6.webp?r=952",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcgZnvtaD-K1zxuS9C5dp6zgPRSBHhSjibTDF56qD51O8W4HTNfs2AFlqwtVYtHQHOeupS0ub9EJsu77-lMXe6jaNrvi7t1QUL8FJgseTYRkM7fM5Tdv_JZ4.webp?r=c99",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABUDKNEB1pBwnu_RXfzP0xnC4k2cz4HulwQ93P4EwhvqT4FXKKhBpljaO4oiDKFmesn0OJbYmmZbXfMzOahtWq0xoWwY0dMXAPTL1zN2DTcfXDYU0r-cglSt6.webp?r=40b",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABbv6q4nKAIJ7y7bny28gw_Qc_s9kTfFdA8bdTP003iaiw6OhBjjNOfzfMhOgqD7Vk7t7DvtQU9IVhSexDSkk7o4Hn-QTJPKsAEoayYwVEVqEqflcW2Do3Fip.webp?r=0b4",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABf8Aw8Af3JDmZBdCiS5fNLuiEVIhduj5mxtLIXQFaJak0retmAR0_eIqkCXlC-hu2YMQD8lvNItvPLEBvBdLfV8uefPXan2rgjjRuh0DZeIiduerzGqd7iJS.webp?r=e5d",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZf3PiaLYl-ER8sWFeIw2Z560fzeTQmAPviwt3YnGMxDfLg63g8u6aUmsHZZH2Z4o6SBYIRyZ2PMo3fFtnP26nr6PhVMn7nX5NDP_PJ8_bEpEd3Ezfmofxgv.webp?r=21c",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABXVM0jmBGYrCi9BUQdr1ExNT9LCJn17WCL9KKp9JuKgH6SzZUFpMl0OacUk7XF4zcVVjuU8RIKVF1TdAJCNYVgErGKgn0WwdNq60WGUoqU_kJVLP3TkUlnIr.webp?r=288",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZMlXG39lo4-qufHnTdLwQaeI6ku5GLsIVRI1vL7filQJem5OE3QpVXdHGRQP9AmCnWuD8ebtP4MDGqcSC4PKSE2Uvx_FPe9GisatKLnxoLARbpxiJgKrnK2.webp?r=2e0"];

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
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABaXkU45ZXupNiPrPU8rKYlhydRnhuxOaU5h6Yt2q-Cr3GFtZ0wxgsr0ZA8sxp_AzylnwTAbV2pcVkHv7PNygp1KVpAIb_2-37qW-zLPJR4WlOi_nnmG0IAz9.webp?r=677",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZEb-1xChJkJI9ZCXz75XwdSwF-P2n9ScrgvwGbS1fb3LG1BIRnxCmSHw8hgYpE3OIa4Fo-4DQZOoqUeTP5B4OI49QPqLpZCfvICDHLm5Nu1HbhrQR8zMZJx.webp?r=22c",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcmSOEcPN1IzcvlzRSZjH-Wr4X_BgRKTITYs4ugqKhA_HXPuC1UzFAxQczq26hLnDSiQF6zzvZsfkd3kpDJ5jmjigw6l0wE_AU45uw_um4pPKgBFMDy9B5MR.webp?r=73b",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZPcezk8r1htr8czZ76-tMP09NbK3lucW-peokfK592jMEGuJVRZuEPny8-y8ysXdPQBq-C2G9xd7d3TrDcU32VB1-yX8gMYdin4hfHfTUTeKLmbhaLS1eGO.webp?r=2b8",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRkhY3pujVZ9oaTqAPHzvBuJ477C_u9Gl3COnlpT88wofxxatd6rsMjGruI-5kImm_AFPu3V0fW8gBS_J-1yRFSXTBnprKqgaQquVRWM2SrzH8PQWqDuo-6u.webp?r=a42",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVyOcT09gIvsvmjeFrx4CdiY8vd277poyEgOxGQz_4SE-MXcYuFjwCPYJjKXp4TvxQ1L59C8jUPxzcBoBRmc4Vveh6FZv3AzrEHv-ttnon53we4RWvkZ66AR.webp?r=069",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVLMhYFs43xmPdceyhVIahmCgWeT6CXiI835RV1kXzKKNsG03YlwoUWeSkIxGYegKxMcOIzw1fFrcWQXE6mKr4tYb9P6ktUvtRbYa0e24fqfA0tRASfavopC.webp?r=d3c",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABRRYxyq9yQHM2oUDbOKMWwG1-rKXFh0CY5Cb3oG7EBiRK2rStjLQYGKo1-cZd1Rzn1jz9GZPJ4TSf9BZoSoqmx9WbGtbMpIIL2NPPSEa8aHNTmxq4zsTfIH-.webp?r=3cd"];

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
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABZ4Fp6QfStTjje80wxPS_or1bQd0FXCn3BMZFNLTGmtga_LhQgVvuFOpxFDBG3ZDEbf9l0Hg3mTHnC6PE4A6soZawwZ-crB_vChv6N_08HVAy0okT8l2lxYt.webp?r=fc3",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABcBTzuAj_vSaHDnIqW5MjWc__KAM_NgexKN6I3k4iaXPicwiR-KQzy1yIUt73hjMieEFNYkHM6HPnkiQus66eqoOiR61b6zF3rHKtwRcQKrB6s00uQB-2T_v.webp?r=fab",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABa2UG7nvU1KTg8nRVkZDUlMX-mEfdZfAOOzJQJrk4mjIvRYVPYtzHfP-wmkDuBobBnicCBG0zj0FodEHkvU8W3h-9HO9rDy6CmfMAY-hSnpEb9bvuc_l2MKB.webp?r=6c0",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABQzgqr2llHi9H31hsVqc2Zpy21LyPVPqF6TejdZvVD13ypgcAl5eMtVrO5fwiqIRUejC2VSe29uOatbZyPo2BnGpa7nTqXtl-21oeAaG2QeryNkPqs1mcUGh.webp?r=a86",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABXh76nHg4o1CXASqP_clIBTdx0Fkk6dLhLE6DJ-zSq_5su0-sLn9TJ8wVnVauB1TlLTX7_UvqqqJC7mYZ7R9pESkNhTPru1B2flvlPFyg6tweRev_D4AUoBS.webp?r=8bb",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABfapnc9qA3dfSkpNNNllsxS0tTTcqQ0Yy0pv5UmS1lRzQRMzxSkbvyVHgRqcMY6iSVpLjhIkrc8Z-GT4BEjZrls9U2jGctSDNLGwZmIoaUyEdDqVK56c9p6g.webp?r=394",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABU4dzDDdtsTASc-LrwfcfjfnPKPoOr57MTb7U4YMMWwYlEtiU7b4kZlgFoOlKp0TTYBxw7wBwYjJzklTV3imArYIF0KhqIbsdBFb1MA5PKgIcFYNBjh-cLXX.webp?r=e8a",
                "https://occ-0-6484-3467.1.nflxso.net/dnm/api/v6/9pS1daC2n6UGc3dUogvWIPMR_OU/AAAABVVv3wLdl-1UKOdzagRzOIrwvMALEWSB-uIyE9PmlnScku9_OB8Q2Tjf9lgH5aTj60vfLHo7J-HoNuFy5Ux_ZEg8Gf-Y6jqoU6gU5CCooPVfYMRI6OifcKHZ.webp?r=7f6",
                "https://resizing.flixster.com/n91pgJMgin35WqlPC5hd7VyBH2M=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24743367_e_h10_aa.jpg",
                "https://resizing.flixster.com/pbNvgvuGRcNZhZefvhzgM3wC8KA=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857692_e_h10_aa.jpg",
                "https://resizing.flixster.com/WlpgXAA-NB960yZwQlBfk2vO1Xg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857696_e_h10_ab.jpg",
                "https://resizing.flixster.com/4ujEIiHMlz1pAt9dW7nDOcLxt_k=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24857701_e_h10_ab.jpg",
                "https://resizing.flixster.com/07LXBRb-Qh69cdaZhVM7wxATHG8=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p24509505_e_h10_aa.jpg"];

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
                "https://resizing.flixster.com/gWATwvDMoCBO6RCoslZdjuQcfus=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31864210_e_h10_aa.jpg",
                "https://resizing.flixster.com/_QM8qaO9b9hy7VvlamBnIeiNOSc=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032077_e_h10_aa.jpg",
                "https://resizing.flixster.com/Bv2ok4Vydt3GGZ5TmzXaFisfCDs=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032078_e_h10_aa.jpg",
                "https://resizing.flixster.com/wxn4iO5Fc5xlb0_s3SG0jumAXSU=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032079_e_h10_aa.jpg",
                "https://resizing.flixster.com/io6pBBjMHjXxiicmwRgPamc3KUM=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032080_e_h10_aa.jpg",
                "https://resizing.flixster.com/2XYMzMtz1MepmLQgBoQAuYrF8jY=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032081_e_h10_aa.jpg",
                "https://resizing.flixster.com/WZhJhlnZVv_paJN2B60icqx-dIg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032082_e_h10_aa.jpg",
                "https://resizing.flixster.com/eNZMj52gB3hog563e8MofsRSqDU=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032083_e_h10_aa.jpg",
                "https://resizing.flixster.com/dhmGxLNsRAI0YfayOO0xW1LXhlg=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p32032084_e_h10_aa.jpg",
                "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg",
                "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg",
                "https://resizing.flixster.com/Xe5sS_qGYTm5lm1iwOEHx-QCTs4=/370x208/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p31595321_i_h10_aa.jpg"];

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
