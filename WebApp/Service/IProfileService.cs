using User.Dto;
using WebApp.Models;
using WebApp.Models;

namespace WebApp.Service;

public interface IProfileService
{
   
    public Task<IPersonalProfile> GetPersonaProfile(Guid id);

    public Task<List<IProfile>> ListProfile(GetProfilesListQuery filterModel,int itemsPerPage, int page);

    public Task<IProfile> GetProfile(Guid id);

    public Task<ISignInResponseDto> AuthByLoginPassword(SignInModelDto signInModelDto);
    
    public Task<UserDto> Registration(RegistrationRequestModelDto registrationRequestModel);
}