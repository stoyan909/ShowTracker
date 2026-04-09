using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IEpisodeServices
    {
        Task SaveEpisodeChanges(Episode episode);

        Task<bool> EpisodeExistInDatabase(int id);

        Task<Episode> GetEpisodeWithSeasons(int id);

        Task DeleteEpisode(Episode episode);

        Episode EditEpisode(EditEpisodeViewModel model);
    }
}
