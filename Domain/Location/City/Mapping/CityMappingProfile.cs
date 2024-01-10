using AutoMapper;
using Location.City.DTO;


namespace Location.City.Mapping;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CreateMap<City, CityDTO>();

        CreateMap<CityDTO, City>()
          .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
          .ForMember(dest => dest.Active, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
          .ForMember(dest => dest.Deleted, opt => opt.Ignore())
          .ForMember(dest => dest.Sort, opt => opt.Ignore())
          .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
    }
}