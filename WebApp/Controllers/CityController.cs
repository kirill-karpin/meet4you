using City.Abstractions;
using City.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

   
    [HttpGet]
    public async Task<CityDTO> Get(Guid id)
    {
        return await _cityService.GetByIdAsync(id);
    }

    


}