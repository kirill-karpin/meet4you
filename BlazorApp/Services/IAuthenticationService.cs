using BlazorJwtAuth.Models;

namespace BlazorApp.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginDTO model);
        Task Logout();
    }
}