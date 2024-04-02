using Location.Country.Abstractions;
using Location.UserLocation;
using Location.UserLocation.Abstractions;
using Location.UserLocation.DTO;
using Location.City.Abstractions;
using AutoMapper;
using Infrastructure;
using Location.Country.DTO;
using Location.City.DTO;
using Location.Country;

namespace Location;

public class LocationService : CrudService<UserLocation.UserLocation, UserLocationDTO, UserLocationDTO, UserLocationDTO>, ILocationService
{
    private readonly IUserLocationRepository _userLocationRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;

    private readonly IMapper _mapper;



    public LocationService( IUserLocationRepository userLocationRepository, ICountryRepository countryRepository, ICityRepository cityRepository, IMapper mapper) 
        :  base(mapper, userLocationRepository)
    {
        _userLocationRepository = userLocationRepository;
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddUserLocation(UserLocationDTO userLocationDTO)
    {
        var userLocation = await _userLocationRepository.GetUserLocation(userLocationDTO.UserId);

        if (userLocation == null)
        {
            userLocation = _mapper.Map<UserLocation.UserLocation>(userLocationDTO);
            
            userLocation.Id = Guid.NewGuid();

            await _userLocationRepository.AddAsync(userLocation);
        }
        else
            _userLocationRepository.Update(userLocation);
        
        await _userLocationRepository.SaveChangesAsync();

        return userLocation.Id;   
    }

    public async Task<List<CountryDTO>> GetCountries()
    {
        var countries =  await  _countryRepository.GetAll();
        return _mapper.Map<List<Country.Country>, List<CountryDTO>>(countries.ToList());

    }

    public async Task<UserLocationDTO> GetUserLocation(Guid userId)
    {
        var userLocation = await _userLocationRepository.GetUserLocation(userId);

        return _mapper.Map<UserLocationDTO>(userLocation);
    }
 
}