using User.Dto;

namespace WebApp.Models;

public class UserProfile : IProfile
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }

    //public List<Photo> Photos { get; set; }
}