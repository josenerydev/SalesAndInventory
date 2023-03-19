using AutoMapper;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
    }
}