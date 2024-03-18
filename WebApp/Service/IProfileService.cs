using User.Dto;
using WebApp.Models;
using WebApp.Models;

namespace WebApp.Service;

public interface IProfileService
{
   
    public Task<IPersonalProfile> GetPersonaProfile(Guid id);

    public Task<List<IProfile>> ListProfile(ListFilterModel filterModel);

    public Task<IProfile> GetProfile(Guid id);

    public Task<ISignInResponseDto> AuthByLoginPassword(SignInModelDto signInModelDto);
    
    public Task<UserDto> Registration(RegistrationRequestModelDto registrationRequestModel);


    // тут начинается внесение данных с IUserService
    public Task<UserDto> Add(UserDto_WithLoginPassword userDto_WithLoginPassword);
    public Task<UserDto> Get(Guid id);
    public Task<UserDto> Update(UserDto userDto);
    public Task<string> GetHashPasswordWithSalt(string unhashedPassword, string salt);
    // Для сброса пароля
    public Task<bool> AddRequestToChangePassword(Guid userId, string password);
    public Task<string> GetConfirmationCode(Guid userId);
    public Task<bool> ChangePassword(Guid userId, string newPassword, string confirmationCode);
    public Task<List<UserDto>> GetTest();
}