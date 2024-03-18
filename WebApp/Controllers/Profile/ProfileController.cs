using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
using WebApp.Models;
using WebApp.Service;
using WebApp.Service;

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

    [Authorize]
    [HttpGet]
    [Route("me")]
    public async Task<IProfile> Me()
    {
        string Id = User.Claims.First(i => i.Type == "Id").Value;
        
        Console.WriteLine($"/api/profile/me Id:{Id}");
        
        return await _profileService.GetPersonaProfile(new Guid(Id));
    }

    [Authorize]
    [HttpGet]
    [Route("list")]
    public async Task<List<IProfile>> List([FromQuery] ListFilterModel filterModel)
    {
        return await _profileService.ListProfile(filterModel);
    }

    [HttpGet]
    [Authorize]
    [Route("get")]
    public async Task<IProfile> Get(Guid id)
    {
        return await _profileService.GetProfile(id);
    }

    // TODO DELETE AFTER TEST
    [HttpGet]
    [Route("getTEST")]
    public async Task<List<UserDto>> GetTest()
    {
        return await _profileService.GetTest();
    }
}