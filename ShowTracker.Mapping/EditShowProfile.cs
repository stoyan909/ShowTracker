using AutoMapper;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Mapping
{
    public class EditShowProfile : Profile
    {
        public EditShowProfile()
        {
            CreateMap<Show, EditShowViewModel>()
                .ForMember(dest => dest.SeasonNumber, opt => opt.MapFrom(src => src.Seasons.Count()));

            CreateMap<EditShowViewModel, Show>();

        }
    }
}
