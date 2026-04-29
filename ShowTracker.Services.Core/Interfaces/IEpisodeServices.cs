using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IEpisodeServices
    {
        Task<int> TotalEpisodesOfShowWatchedAsync(Guid showId, Guid userId);

        Task SaveEpisodeChanges(Episode episode);

        Task WatchedEpisode(int id, Guid userId, bool hasWatched);

        Task<bool> EpisodeExistInDatabase(int id);

        Task<bool> EpisodeAlreadyWatchedByUser(int id, Guid userId);

        Task<Episode?> GetEpisodeWithSeasons(int id);

        Task DeleteEpisode(Episode episode);

        Episode EditEpisode(EditEpisodeViewModel model);
    }
}
