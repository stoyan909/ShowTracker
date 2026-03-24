namespace ShowTracker.Common
{
    public class ValidationConstants
    {
        public class EpisodesValidationConstants
        {
            public const int EpisodeTitleMinLength = 2;
            public const int EpisodeTitleMaxLength = 40;
        }

        public class ShowsValidationConstants
        {
            public const int ShowNameMinLength = 3;
            public const int ShowNameMaxLength = 60;

            public const int ShowDescriptionMinLength = 10;
            public const int ShowDescriptionMaxLength = 500;
        }
    }
}
