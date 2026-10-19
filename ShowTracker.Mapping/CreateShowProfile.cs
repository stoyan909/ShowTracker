using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.ShowsViewModel;

namespace ShowTracker.Mapping
{
    public class CreateShowProfile:Profile
    {
        public CreateShowProfile()
        {
            CreateMap<CreateShowViewModel, Show>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Genre, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
