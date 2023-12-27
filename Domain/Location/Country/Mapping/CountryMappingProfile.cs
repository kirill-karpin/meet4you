using AutoMapper;
using Location.Country.DTO;


namespace Location.Country.Mapping;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryDTO>();
    }
}