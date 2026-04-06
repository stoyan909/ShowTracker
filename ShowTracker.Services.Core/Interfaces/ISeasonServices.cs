using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface ISeasonServices
    {
        Task<Season> GetSeason(Guid id);

        Task<bool> SeasonExistInDataBase(Guid id);

        Task SaveSeasonChanges(Season season);

        Season AddNewEpisodeToSeason(Season season,CreateEpisodeViewModel model);
    }
}
