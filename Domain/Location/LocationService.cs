using Location.Country.Abstractions;
using Location.UserLocation;
using Location.UserLocation.Abstractions;
using Location.UserLocation.DTO;
using Location.City.Abstractions;
using AutoMapper;
using Infrastructure;

namespace Location;

public class LocationService : CrudService<UserLocation.UserLocation, UserLocationDTO, UserLocationDTO, UserLocationDTO>, ILocationService
{
    private readonly IUserLocationRepository _userLocationRepository;
    private readonly IMapper _mapper;


    public LocationService( IUserLocationRepository userLocationRepository, IMapper mapper) :  base(mapper, userLocationRepository)
    {
        _userLocationRepository = userLocationRepository;
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

    public async Task<UserLocationDTO> GetUserLocation(Guid userId)
    {
        var userLocation = await _userLocationRepository.GetUserLocation(userId);

        return _mapper.Map<UserLocationDTO>(userLocation);
    }
    public async Task<List<UserLocationDTO>> GetAll()
    {
        var locations = await _userLocationRepository.GetAllAsync();

        return _mapper.Map<List<UserLocation.UserLocation>, List<UserLocationDTO>>(locations);
    }
}