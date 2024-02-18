using Message.Dto;
using Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
using User;
using System.Configuration;
using WebApi.Settings;
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

        return await _userService.GetHashPasswordWithSalt(password, null);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<UserDto> Update(UserDto userDto)
    {
        return await _userService.Update(userDto);
    }

    [HttpGet]
    [Route("GetConfirmationCode")]
    public async Task<string> GetConfirmationCode(Guid userId)
    {
        return await _userService.GetConfirmationCode(userId);
    }

    [HttpPost]
    [Route("AddRequestToChangePassword")]
    public async Task<bool> AddRequestToChangePassword(Guid userId, string newPassword)
    {
        return await _userService.AddRequestToChangePassword(userId, newPassword);
    }

    [HttpPost]
    [Route("ChangePassword")]
    public async Task<bool> ChangePassword(Guid userId, string newPassword, string confirmationCode)
    {
        return await _userService.ChangePassword(userId, newPassword, confirmationCode);
    }
}