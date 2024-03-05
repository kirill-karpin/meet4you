using Location.City.DTO;
using Location.Country.Abstractions;
using Location.Country.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers.Location;

[ApiController]
[Route("country")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var countryDTO = await _countryService.GetByIdAsync(id);

        return countryDTO == null
           ? NotFound()
           : Ok(countryDTO);
    }

    [HttpGet]
    [Route("get-countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _countryService.GetPagedAsync(1, 100);

        return countries == null
           ? BadRequest()
           : Ok(countries);
    }

    [HttpPost]
    [Route("add-country")]
    public async Task<IActionResult> AddCountry(CreateOrUpdateCountryDTO request)
    {
        request.Id = Guid.NewGuid();

        var guId = await _countryService.CreateAsync(request);

        return guId == Guid.Empty
            ? BadRequest()
            : Ok(guId);
    }

    [HttpDelete]
    [Route("delete-country")]
    public async Task<IActionResult> DeleteCountry(Guid id)
    {
        await _countryService.DeleteAsync(id);

        return Ok();
    }





}