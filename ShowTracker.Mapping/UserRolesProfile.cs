using AutoMapper;
using ShowTracker.Data.Models;
using ShowTracker.ViewModel.Admin;

namespace ShowTracker.Mapping
{
    public class UserRolesProfile : Profile
    {
        public UserRolesProfile() 
        {
            CreateMap<ApplicationUser, UsersAndRolesViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
