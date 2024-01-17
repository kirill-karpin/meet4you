namespace WebApp.Models;

public class SignInResponseDto : ISignInResponseDto
{
    public string Token { set; get; }
    public string RefreshToken { set; get; }
}