using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Location.UserLocation.Abstractions;

namespace Location.UserLocation;

public class UserLocationRepository : CrudRepository<UserLocation>, IUserLocationRepository
{
    public UserLocationRepository(DbContext context) : base(context)
    {
    }

    public async Task<List<UserLocation>> GetAllAsync()
    {
        return await _context.Set<UserLocation>()
            .Include(u => u.City)
            .Include(u => u.Country)
            .ToListAsync();
    }

    public async Task<UserLocation> GetUserLocation(Guid userId)
    {
        return await _context.Set<UserLocation>()
            .Include(x => x.City)
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }




}