using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthenticationController: Controller
{
    [HttpPost]
    [Route("sign-in")]
    public IActionResult SignIn()
    {
        return Ok("ok");
    }
    
    [HttpPost]
    [Route("registration")]
    public IActionResult Registration()
    {
        return Ok("ok");
    }
    
    [HttpPost]
    [Route("reset-password")]
    public IActionResult ResetPassword()
    {
        return Ok("ok");
    }
}