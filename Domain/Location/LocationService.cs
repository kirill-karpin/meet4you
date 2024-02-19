using Location.Country.Abstractions;
using Location.UserLocation;
using Location.UserLocation.Abstractions;
using Location.UserLocation.DTO;
using Location.City.Abstractions;
using AutoMapper;
using Infrastructure;
using User.Abstraction;
using User;
using AutoMapper.Configuration.Annotations;

namespace Location;

public class LocationService : CrudService<UserLocation.UserLocation, UserLocationDTO, UserLocationDTO, UserLocationDTO>, ILocationService
{
    private readonly IUserLocationRepository _userLocationRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public LocationService( IUserLocationRepository userLocationRepository, 
        ICountryRepository countryRepository,
        ICityRepository cityRepository,
        IUserRepository userRepository,
        IMapper mapper) :  base(mapper, userLocationRepository)
    {
        _userLocationRepository = userLocationRepository;
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
        _userRepository= userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddUserLocation(UserLocationDTO userLocationDTO)
    {
        var country =await _countryRepository.GetAsync(userLocationDTO.CountryId);
        if (country==null)
            throw new KeyNotFoundException("Country not found");

        var city = await _cityRepository.GetAsync(userLocationDTO.CityId);
        if (city == null)
            throw new KeyNotFoundException("City not found");

        var user = _userRepository.Get(userLocationDTO.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        var userLocations = await _userLocationRepository.GetPagedAsync(1, 100);

        var userLocation = userLocations.FirstOrDefault(x => x.UserId == userLocationDTO.UserId);

        if (userLocation == null)
        {
            userLocation = _mapper.Map<UserLocation.UserLocation>(userLocationDTO);
            
            userLocation.Id = Guid.NewGuid();

            await _userLocationRepository.AddAsync(userLocation);
        }
        else
            _userLocationRepository.Update(userLocation);
        
        await _userRepository.SaveChangesAsync();

        return userLocation.Id;   
    }

    public async Task<UserLocationDTO> GetUserLocation(Guid userId)
    {
        var userLocation = await _userLocationRepository.GetUserLocation(userId);

        if (userLocation==null)
            throw new KeyNotFoundException("UserLocation not found");

        return _mapper.Map<UserLocationDTO>(userLocation);
    }
    public async Task<List<UserLocationDTO>> GetAll()
    {
        var locations = await _userLocationRepository.GetAllAsync();

        return _mapper.Map<List<UserLocation.UserLocation>, List<UserLocationDTO>>(locations);
    }
}