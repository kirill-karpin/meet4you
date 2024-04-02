using Location.City.Abstractions;
using Location.City.DTO;
using Location.Country.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Location;

[ApiController]
[Route("city")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    [Route("get-cities")]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _cityService.GetPagedAsync(1, 100);

        return cities == null
           ? BadRequest()
           : Ok(cities);
    }

    [HttpGet]
    [Route("get-cities/{countryId}")]
    public async Task<IActionResult> GetCities(Guid countryId)
    {
        var cities = await _cityService.GetCitiesByCountryId(countryId);

        return cities == null
           ? BadRequest()
           : Ok(cities);
    }
}