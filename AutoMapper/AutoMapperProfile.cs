using AutoMapper;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.AutoMapper
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
