using System.ComponentModel.DataAnnotations;
using BlazorJwtAuth.Models.Statuses;

namespace BlazorJwtAuth.Models;

public class RegistrationDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string Surname { get; set; }
    public string LastName { get; set; }
    
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
        
    public string LookingFor { get; set; }   
    public bool Gender { get; set; }
    public FamilyStatus FamilyStatus { get; set; }
    public bool HaveChildren { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string About { get; set; }
    [Required]
    public string Email { get; set; }
}