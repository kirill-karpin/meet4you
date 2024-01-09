using City.DTO;
using Country.Abstractions;
using Country.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("{id}")]
    public async Task<CountryDTO> GetById(Guid id)
    {
        return await _countryService.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<List<CountryDTO>> GetAll()
    {
        return await _countryService.GetAllAsync();
    }
}