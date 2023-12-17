using Microsoft.AspNetCore.Mvc;
using User;
using User.Dto;
using WebApi.Models;
using WebApi.Service;
using WebApp.Models;

namespace WebApp.Controllers.Auth;

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
    public async Task<SignInResponseDto> SignIn(SignInModelDto signInModel)
    {
        IProfile profile = await this._profileService.AuthByLoginPassword(signInModel);

        return await Task.FromResult(new SignInResponseDto()
        {
            Token = "secret_token",
            RefresthToken = "secret_refresh_token"
        });
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