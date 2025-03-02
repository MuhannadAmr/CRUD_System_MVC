using AutoMapper;
using MVCProject.DAL.Models;
using MVCProject.PL.Models;

namespace MVCProject.PL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
