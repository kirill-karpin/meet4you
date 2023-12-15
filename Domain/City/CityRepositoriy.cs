using City.Abstractions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace City;

public class CityRepository : CrudRepository<City>, ICityRepository
{
    public CityRepository(DbContext context) : base(context)
    {
    }
}