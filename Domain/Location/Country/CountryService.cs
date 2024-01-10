using Location.Country.Abstractions;
using AutoMapper;
using Location.Country.DTO;
using Infrastructure;
using Location.City.Abstractions;
using Location.City.DTO;
using System.Linq.Expressions;
using System.Xml;

namespace Location.Country;

public class CountryService : CrudService<Country, CreateOrUpdateCountryDTO, CreateOrUpdateCountryDTO, CountryDTO>, ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryService(ICountryRepository countryRepository, IMapper mapper) : base(mapper,countryRepository)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    
}