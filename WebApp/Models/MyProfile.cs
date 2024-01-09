using User.Dto;

namespace WebApi.Models;

public class MyProfile : IOwnProfile
{
    public Guid Id { get; set; }

    public User.User User { get; set; }

    public List<Message.Message> Messages { get; set; }

    //public List<Photo> Photos { get; set; }

    public string Settings { get; set; }
}