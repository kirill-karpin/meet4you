using Entities.Abstractions;

namespace Location.Country;

public class Country : BaseEntity
{
    public string Name { get; set; }

    public virtual IEnumerable<City.City> Cities { get; set; }
}