using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
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

    [HttpGet]
    [Route("list")]
    public async Task<List<UserProfile>> List([FromQuery] GetProfilesListQuery query, [Required] int itemsPerPage, [Required] int page)
    {
        return await _profileService.ListProfile(query, itemsPerPage, page);
    }

    [HttpGet]
    [Authorize]
    [Route("{id}")]
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

    [HttpGet]
    [Route("add-fake-users")]
    public async Task<IActionResult> AddFakeUsers()
    {
         await _profileService.AddFakeUsers();
         return Ok();


    }
    
    [Authorize]
    [HttpPost]
    [Route("update")]
    public async Task<UserDto> Update(UserDto userDto)
    {
        return await _profileService.Update(userDto);
    }

}