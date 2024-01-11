using Location.City.Abstractions;
using AutoMapper;
using Infrastructure;
using Location.City.DTO;

namespace Location.City;

public class CityService : CrudService<City, CityDTO, CityDTO, CityDTO>, ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public CityService(IMapper mapper, ICityRepository cityRepository) : base(mapper, cityRepository)
    {
    }


}