using Country.DTO;

namespace Country.Abstractions;

public interface ICountryService
{
    Task<List<CountryDTO>> GetAllAsync();
    Task<CountryDTO> GetByIdAsync(Guid id);
}