using AutoMapper;
using MVCProject.DAL.Models;
using MVCProject.PL.Models;

namespace MVCProject.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
