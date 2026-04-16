using AutoMapper;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Mapping
{
    public class CreateEpisodeProfile:Profile
    {
        public CreateEpisodeProfile()
        {
            CreateMap<CreateEpisodeViewModel, Episode>();
        }
    }
}
