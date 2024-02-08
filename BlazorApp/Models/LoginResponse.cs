namespace BlazorJwtAuth.Models
{
    public class LoginResponse:Status
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
