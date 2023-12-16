using AutoMapper;
using City.DTO;


namespace City.Mapping;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityDTO>();
    }
}