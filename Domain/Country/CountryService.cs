using Country.Abstractions;
using AutoMapper;
using Infrastructure;
using System.Data.Entity;
using Country.DTO;

namespace Country;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryService(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public Task<List<CountryDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CountryDTO> GetByIdAsync(Guid id)
    {
        var country = await _countryRepository.GetAsync(id);
        return _mapper.Map<Country, CountryDTO>(country);
    }
}