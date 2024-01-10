using AutoMapper;

namespace Location.UserLocation.Mapping;

public class UserLocationMappingProfile : Profile
{
    public UserLocationMappingProfile()
    {
        CreateMap<UserLocation, DTO.UserLocationDTO>();
    }
}