using Location.City.Abstractions;
using AutoMapper;
using Infrastructure;
using Location.City.DTO;
using System.Data.Entity;

namespace Location.City;

public class CityService : CrudService<City, CityDTO, CityDTO, CityDTO>, ICityService
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public CityService(IMapper mapper, ICityRepository cityRepository) : base(mapper, cityRepository)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<List<CityDTO>> GetCitiesByCountryId(Guid countryId)
    {
        var cities = await _cityRepository.GetAllAsync(CancellationToken.None);
        return _mapper.Map<List<City>,List<CityDTO>>(cities.Where(c=>c.CountryId == countryId).ToList());
    }
}