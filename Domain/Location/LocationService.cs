using Location.Country.Abstractions;
using AutoMapper;
using Location.Country.DTO;
using Location.UserLocation.Abstractions;
using Location.City.DTO;
using Location.UserLocation.DTO;
using Location.City.Abstractions;
using Location.UserLocation;

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

    public async Task<UserLocationDTO> AddUserLocation(UserLocationDTO userLocationDTO)
    {
        var user = await _userLocationRepository.AddAsync(_mapper.Map<UserLocation.UserLocation>(userLocationDTO));
        return userLocationDTO;
            
    }

    public Task<UserLocationDTO> GetUserLocation(Guid userId)
    {
        throw new NotImplementedException();
    }
}