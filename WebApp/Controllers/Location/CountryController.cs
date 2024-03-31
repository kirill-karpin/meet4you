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
    [Route("get-countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _countryService.GetPagedAsync(1, 100);

        return countries == null
           ? BadRequest()
           : Ok(countries);
    }



  





}