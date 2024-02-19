using Infrastructure.Abstraction;
using Location.UserLocation.DTO;

namespace Location.UserLocation.Abstractions;

public interface ILocationService : ICrudService<UserLocation, UserLocationDTO, UserLocationDTO, UserLocationDTO>
{
    Task<Guid> AddUserLocation(UserLocationDTO userLocationDTO);
    Task<UserLocationDTO> GetUserLocation(Guid userId);
    Task<List<UserLocationDTO>> GetAll();
}