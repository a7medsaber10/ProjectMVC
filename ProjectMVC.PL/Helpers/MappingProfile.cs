using AutoMapper;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.ViewModels;

namespace ProjectMVC.PL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
