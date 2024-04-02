using Location.Country.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Location.Country.DTO;

namespace Location.Country;

public class CountryRepository : CrudRepository<Country>, ICountryRepository
{
    public CountryRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Country>> GetAll()
    {
        return await _context.Set<Country>()
            .Include(c => c.Cities)
            .ToListAsync();
    }

    public async override Task<Country> GetAsync(Guid id)
    {
        return await _context.Set<Country>()
            .Include(c => c.Cities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}