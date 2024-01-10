using Location.City.DTO;
using Location.Country.DTO;
using Location.UserLocation.DTO;

namespace Location.UserLocation.Abstractions;

public interface ILocationService
{
    Task<UserLocationDTO> AddUserLocation(UserLocationDTO userLocationDTO);

    Task<UserLocationDTO> GetUserLocation(Guid userId);
}