using AutoMapper;
using SalesAndInventory.Models;
using SalesAndInventory.Shared.Dtos;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ReverseMap();
    }
}