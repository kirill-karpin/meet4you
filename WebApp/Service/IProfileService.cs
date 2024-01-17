using User.Dto;
using WebApi.Models;
using WebApp.Models;

namespace WebApp.Service;

public interface IProfileService
{
    public Task<IOwnProfile> GetOwnProfile();

    public Task<List<IProfile>> ListProfile();

    public Task<IProfile> GetProfile(Guid id);

    public Task<ISignInResponseDto> AuthByLoginPassword(SignInModelDto signInModelDto);
    
    public Task<UserDto> Registration(RegistrationRequestModelDto registrationRequestModel);
}