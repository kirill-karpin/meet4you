using Location.Country.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Location.Country;

public class CountryRepository : CrudRepository<Country>, ICountryRepository
{
    public CountryRepository(DbContext context) : base(context)
    {
    }

    public async override Task<Country> GetAsync(Guid id)
    {
        return await _context.Set<Country>()
            .Include(c => c.Cities)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}