using AutoMapper;
using MVCProject.DAL.Models;
using MVCProject.PL.Models;

namespace MVCProject.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
