using Message.Dto;
using User.Dto;

namespace Common;

public class Profile
{
    public Guid Id { get; set; }

    public UserDto User { get; set; }

    public  ICollection<MessageDto> Messages { get; set; }

    //public List<Photo> Photos { get; set; }

    public string Settings { get; set; }
}