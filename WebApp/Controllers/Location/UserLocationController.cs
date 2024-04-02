using Location.City.Abstractions;
using Location.UserLocation.Abstractions;
using Location.UserLocation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Location;

[ApiController]
[Route("user-location")]
public class UserLocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public UserLocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    [Route("get-user-location")]
    public async Task<IActionResult> GetUserLocation(Guid userId)
    {
        var userLocation = await _locationService.GetUserLocation(userId);

        return userLocation == null
           ? NotFound()
           : Ok(userLocation);
    }

    [HttpPost]
    [Route("add-user-location")]
    public async Task<IActionResult> AddUserLocation(UserLocationDTO userLocationDTO)
    {
        var guId = await _locationService.AddUserLocation(userLocationDTO);

        return guId == Guid.Empty
            ? BadRequest()
            : Ok(guId);
    }

  


}