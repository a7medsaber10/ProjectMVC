using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectMVC.DAL.Models;
using ProjectMVC.PL.ViewModels;

namespace ProjectMVC.PL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>().ForMember(d=>d.RoleName, o=>o.MapFrom(s=>s.Name)).ReverseMap();
        }
    }
}
