using Entities.Abstractions;

namespace Country;

public class Country : BaseEntity
{
    public string Name { get; set; }

    public virtual IEnumerable<City.City> Cities { get; set; }
}