using Location.Country.DTO;

namespace Location.Country.Abstractions;

public interface ICountryService
{
    Task<List<CountryDTO>> GetAllAsync();
    Task<CountryDTO> GetByIdAsync(Guid id);
}