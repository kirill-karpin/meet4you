using Location.City.Abstractions;
using Location.City.DTO;
using Location.Country.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Location;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cityDTO = await _cityService.GetByIdAsync(id);

        return cityDTO == null
           ? NotFound()
           : Ok(cityDTO);
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _cityService.GetPagedAsync(1, 100);

        return cities == null
           ? BadRequest()
           : Ok(cities);
    }


    [HttpPost]
    public async Task<IActionResult> AddCity(CityDTO request)
    {
        request.Id = Guid.NewGuid();

        var guId = await _cityService.CreateAsync(request);

        return guId == Guid.Empty
            ? BadRequest()
            : Ok(guId);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCity(Guid id)
    {
        await _cityService.DeleteAsync(id);

        return Ok();
    }
}