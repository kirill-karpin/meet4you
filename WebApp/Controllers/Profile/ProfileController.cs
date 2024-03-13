using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public async Task<List<IProfile>> List([FromQuery] GetProfilesListQuery query, [Required] int itemsPerPage, [Required] int page)
    {
        return await _profileService.ListProfile(query, itemsPerPage, page);
    }

    [HttpGet]
    [Authorize]
    [Route("get")]
    public async Task<IProfile> Get(Guid id)
    {
        return await _profileService.GetProfile(id);
    }
}