using WebApi.Models;

namespace WebApi.Service;

public interface IProfileService
{
    public Task<IOwnProfile> GetOwnProfile();
    
    public Task<List<IProfile>> ListProfile();
    
    public Task<IProfile> GetProfile(Guid id);
}