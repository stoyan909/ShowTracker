using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTracker.Data.Models;

namespace ShowTracker.Services.Core.Interfaces
{
    public interface IEpisodeServices
    {
        Task<bool> EpisodeExistInDatabase(int id);

        Task<Episode> GetEpisode(int id);

        Task DeleteEpisode(Episode episode);
    }
}
