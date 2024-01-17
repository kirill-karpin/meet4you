using Message.Dto;
using Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
using User;
using System.Configuration;
using WebApp.Settings;
using System.Security.Policy;

namespace WebApp.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public UserController(IUserService iUserService, IConfiguration configuration)
    {
        _userService = iUserService;
        _configuration = configuration;
    }


    [HttpPost]
    [Route("Add")]
    public async Task<UserDto> Add(UserDto_WithLoginPassword userDto_WithLoginPassword)
    {
        return await _userService.Add(userDto_WithLoginPassword);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto> Get(Guid id)
    {
        return await _userService.Get(id);
    }

    [HttpGet]
    [Route("GetHash")]
    public async Task<string> GetHashFromPassword(string password)
    {
        string? hashWord = _configuration["DefaultHash"];

        return await _userService.GetHashPasswordWithSalt(password, hashWord);
    }
}