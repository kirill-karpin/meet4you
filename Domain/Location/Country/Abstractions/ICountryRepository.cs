using Infrastructure.Abstraction;

namespace Location.Country.Abstractions;

public interface ICountryRepository : ICrudRepository<Country>
{
}