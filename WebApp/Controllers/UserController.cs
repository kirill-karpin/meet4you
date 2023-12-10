using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Dto;

namespace WebApp.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("Add")]
    public async Task<UserDto> Add(UserDto userDto)
    {
        throw new NotImplementedException("Не запилили");
    }
}