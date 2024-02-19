using AutoMapper;
using Location.UserLocation.DTO;

namespace Location.UserLocation.Mapping;

public class UserLocationMappingProfile : Profile
{
    public UserLocationMappingProfile()
    {
        CreateMap<UserLocationDTO, UserLocation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.Sort, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore());
        CreateMap<UserLocation, UserLocationDTO>();
    }
}