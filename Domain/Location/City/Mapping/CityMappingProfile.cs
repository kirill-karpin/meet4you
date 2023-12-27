using AutoMapper;
using Location.City.DTO;


namespace Location.City.Mapping;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityDTO>();
    }
}