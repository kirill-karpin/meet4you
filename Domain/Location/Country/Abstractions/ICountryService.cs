using Infrastructure.Abstraction;
using Location.Country.DTO;

namespace Location.Country.Abstractions;

public interface ICountryService : ICrudService<Country, CreateOrUpdateCountryDTO, CreateOrUpdateCountryDTO, CountryDTO>
{
   
}