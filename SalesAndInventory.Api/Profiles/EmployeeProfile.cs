using AutoMapper;
using SalesAndInventory.Api.Dtos;
using SalesAndInventory.Api.Models;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ReverseMap();
    }
}