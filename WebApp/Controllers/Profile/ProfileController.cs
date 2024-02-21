﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<List<IProfile>> List()
    {
        return await _profileService.ListProfile();
    }

    [HttpGet]
    [Authorize]
    [Route("get")]
    public async Task<IProfile> Get(Guid id)
    {
        return await _profileService.GetProfile(id);
    }
}