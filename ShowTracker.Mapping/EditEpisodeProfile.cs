using AutoMapper;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.EpisodesViewModel;

namespace ShowTracker.Mapping
{
    public class EditEpisodeProfile : Profile
    {
        public EditEpisodeProfile()
        {
            CreateMap<Episode,EditEpisodeViewModel>();
        }
    }
}
