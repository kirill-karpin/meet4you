using BlazorJwtAuth.Models;

namespace BlazorApp.Services
{
    public interface IAuthenticationService
    {
        Task<HttpResponseMessage> Registration(RegistrationDTO model);
        Task<LoginResponse> Login(LoginDTO model);
        Task Logout();
        Task<bool> IsAuthorized();
        Task<string> GetUserId();
    }
}