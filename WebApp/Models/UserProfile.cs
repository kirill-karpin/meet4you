using User.Dto;

namespace WebApi.Models;

public class UserProfile : IProfile
{
    public Guid Id { get; set; }

    private User.User User { get; set; }

    //public List<Photo> Photos { get; set; }
}