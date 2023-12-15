using Entities.Abstractions;

namespace City;

public class City : BaseEntity
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }
}