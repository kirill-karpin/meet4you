using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Location.UserLocation.Abstractions;

namespace Location.UserLocation;

public class UserLocationRepository : CrudRepository<UserLocation>, IUserLocationRepository
{
    public UserLocationRepository(DbContext context) : base(context)
    {
    }
}