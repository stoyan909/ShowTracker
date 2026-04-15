using AutoMapper;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.ShowsViewModel;

namespace Mapping
{
    public class CreateShowProfile:Profile
    {
        public CreateShowProfile()
        {
            CreateMap<CreateShowViewModel, Show>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
