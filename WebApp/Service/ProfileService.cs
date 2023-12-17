using Message;
using Message.Abstraction;
using User;
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
        return await Task.FromResult<IOwnProfile>(new MyProfile() { Id = new Guid("bf470537-afd5-4849-ab8e-174961288d10")});
    }

    public async Task<List<IProfile>> ListProfile()
    {
        return await Task.FromResult(new List<IProfile>()
        {
            new UserProfile() { Id = new Guid("bf470537-afd5-4849-ab8e-174961288d10")}
        });
    }

    public async Task<IProfile> GetProfile(Guid id)
    {
        return await Task.FromResult<IProfile>(new UserProfile()
        {
            Id = id
        });
    }
}