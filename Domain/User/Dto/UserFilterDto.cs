namespace User.Dto;

public class UserFilterDto
{
    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }
    public bool? Gender { get; set; }
    public FamilyStatus? FamilyStatus { get; set; }
    public bool? HaveChildren { get; set; }
}

