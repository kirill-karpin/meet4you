using Infrastructure.Abstraction;
using Location.Country.DTO;

namespace Location.Country.Abstractions;

public interface ICountryRepository : ICrudRepository<Country>
{
    Task<IEnumerable<Country>> GetAll();
}