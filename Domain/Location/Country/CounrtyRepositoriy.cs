using Location.Country.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Location.Country;

public class CountryRepository : CrudRepository<Country>, ICountryRepository
{
    public CountryRepository(DbContext context) : base(context)
    {
    }
}