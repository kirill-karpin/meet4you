using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Service;

namespace WebApp.Controllers.Profile;

[ApiController]
[Route("api/profile")]
public class ProfileController : Controller
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    [Route("me")]
    public async Task<IProfile> Me()
    {
        return await _profileService.GetOwnProfile();
    }


    [HttpGet]
    [Route("list")]
    public async Task<List<IProfile>> List()
    {
        return await _profileService.ListProfile();
    }

    [HttpGet]
    [Route("get")]
    public async Task<IProfile> Get(Guid id)
    {
        return await _profileService.GetProfile(id);
    }
}