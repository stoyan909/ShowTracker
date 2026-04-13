using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IEpisodeServices
    {
        Task SaveEpisodeChanges(Episode episode);

        Task MarkEpisodeAsWatched(int id, string userId);

        Task UnmarkEpisodeAsWatched(int id, string userId);

        Task<bool> EpisodeExistInDatabase(int id);

        Task<bool> EpisodeAlreadyWatchedByUser(int id, string userId);

        Task<Episode> GetEpisodeWithSeasons(int id);

        Task DeleteEpisode(Episode episode);

        Episode EditEpisode(EditEpisodeViewModel model);
    }
}
