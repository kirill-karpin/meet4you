using Location.City.DTO;

namespace Location.Country.DTO;

public class CountryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CityDTO> Cities { get; set; }
}