using Message.Dto;
using Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
using User;

namespace WebApp.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService iUserService)
    {
        _userService = iUserService;
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
}