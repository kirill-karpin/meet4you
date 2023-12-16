using City.Abstractions;
using AutoMapper;
using Infrastructure;
using City.DTO;

namespace City;

public class CityService : CrudService<City, CreatingCityDTO, UpdatingCityDTO, CityDTO>, ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public CityService(IMapper mapper,ICityRepository cityRepository) : base(mapper, cityRepository)
    {
    }
}