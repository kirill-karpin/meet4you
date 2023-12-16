using AutoMapper;
using Country.DTO;


namespace Country.Mapping;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryDTO>();
    }
}