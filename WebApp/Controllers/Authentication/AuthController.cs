using Install;
using Microsoft.AspNetCore.Mvc;
using User.Dto;
using WebApp.Models;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers.Authentication;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : Controller
{
    private readonly IProfileService _profileService;
    
    public AuthenticationController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<ISignInResponseDto> SignIn(SignInModelDto signInModelDto)
    {
        try
        {
            var tokens = await _profileService.AuthByLoginPassword(signInModelDto);
            return await Task.FromResult(tokens);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Registration(RegistrationRequestModelDto registrationRequestModelDto)
    {
        try
        {
            UserDto newUser = await _profileService.Registration(registrationRequestModelDto);
            return Ok(newUser.Id);
        }
        catch (Exception e)
        {
            return BadRequest("error");    
        }
    }

    [HttpPost]
    [Route("reset-password")]
    public IActionResult ResetPassword()
    {
        return Ok("ok");
    }
}