using Infrastructure.Abstraction;

namespace Location.UserLocation.Abstractions;

public interface IUserLocationRepository : ICrudRepository<UserLocation>
{
    Task<UserLocation> GetUserLocation(Guid userId);
    Task<List<UserLocation>> GetAllAsync();
}