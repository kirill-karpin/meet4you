using Location.City.DTO;

namespace Location.Country.DTO;

public class CreateOrUpdateCountryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}