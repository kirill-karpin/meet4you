using User.Dto;

namespace WebApp.Models;

public class UserProfile : IProfile
{
    public Guid Id { get; set; }

    private User.User User { get; set; }

    //public List<Photo> Photos { get; set; }
}