using User;

namespace WebApi.Models;

public class RegistrationRequestModelDto
{


    public bool Gender { get; set; }
    public FamilyStatus FamilyStatus { get; set; }
    public bool HaveChildren { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Login { get; set; }
    public string LookingFor { get; set; }
    public string About { get; set; }
}