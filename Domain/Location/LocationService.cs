using Location.Country.Abstractions;
using AutoMapper;
using Location.Country.DTO;
using Location.UserLocation.Abstractions;
using Location.City.DTO;
using Location.UserLocation.DTO;
using Location.City.Abstractions;
using Location.UserLocation;
using System.Data.Entity;

namespace Location;

public class LocationService : ILocationService
{
    private readonly IUserLocationRepository _userLocationRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;


    public LocationService( IUserLocationRepository userLocationRepository, 
        ICountryRepository countryRepository,
        ICityRepository cityRepository, 
        IMapper mapper)
    {
        _userLocationRepository = userLocationRepository;
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddUserLocation(UserLocationDTO userLocationDTO)
    {
        var country = _countryRepository.GetAsync(userLocationDTO.CountryId);
        if (country==null)
            throw new KeyNotFoundException("Country not found");

        var city = _cityRepository.GetAsync(userLocationDTO.CityId);
        if (city == null)
            throw new KeyNotFoundException("City not found");

        var userLocation = _mapper.Map<UserLocation.UserLocation>(userLocationDTO);

         await _userLocationRepository.AddAsync(userLocation);

        return userLocation.Id;   
    }

    public async Task<UserLocationDTO> GetUserLocation(Guid userId)
    {
        var userLocations = await _userLocationRepository.GetPagedAsync(1, 100);

        var userLocation = userLocations.FirstOrDefault(x => x.UserId == userId);

        if (userLocation==null)
            throw new KeyNotFoundException("UserLocation not found");

        return _mapper.Map<UserLocationDTO>(userLocation);
    }
}