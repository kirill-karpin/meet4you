using Message;
using Message.Abstraction;
using Microsoft.AspNetCore.Mvc;
using User;
using User.Dto;
using WebApi.Models;

namespace WebApi.Service;

public class ProfileService : IProfileService
{
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;

    public ProfileService(IUserService userService, IMessageService messageService)
    {
        _userService = userService;
        _messageService = messageService;
    }

    public async Task<IOwnProfile> GetOwnProfile()
    {
        return await Task.FromResult<IOwnProfile>(new MyProfile()
            { Id = new Guid("bf470537-afd5-4849-ab8e-174961288d10") });
    }

    public async Task<List<IProfile>> ListProfile()
    {
        return await Task.FromResult(new List<IProfile>()
        {
            new UserProfile() { Id = new Guid("bf470537-afd5-4849-ab8e-174961288d10") }
        });
    }

    public async Task<IProfile> GetProfile(Guid id)
    {
        return await Task.FromResult<IProfile>(new UserProfile()
        {
            Id = id
        });
    }

    public async Task<IProfile> AuthByLoginPassword(SignInModelDto signInModelDto)
    {
        return await Task.FromResult<IOwnProfile>(new MyProfile()
            { Id = new Guid("bf470537-afd5-4849-ab8e-174961288d10") });
    }

    public async Task<UserDto> Registration(RegistrationRequestModelDto registrationRequestModel)
    {
        return  await _userService.Add(new UserDto_WithLoginPassword()
        {
            Id =  Guid.NewGuid(),
            About = registrationRequestModel.About,
            Blocked = false,
            Gender = registrationRequestModel.Gender,
            Login = registrationRequestModel.Login,
            FirstName = registrationRequestModel.FirstName,
            LastName = registrationRequestModel.LastName,
            Password = registrationRequestModel.Password,
            Surname = registrationRequestModel.Surname,
            DateOfBirth = registrationRequestModel.DateOfBirth,
            LookingFor = registrationRequestModel.LookingFor,
            FamilyStatus = registrationRequestModel.FamilyStatus,
            HaveChildren = registrationRequestModel.HaveChildren,
        });
    }
}