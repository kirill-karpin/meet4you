using Infrastructure.Abstraction;

namespace Location.UserLocation.Abstractions;

public interface IUserLocationRepository : ICrudRepository<UserLocation>
{
}