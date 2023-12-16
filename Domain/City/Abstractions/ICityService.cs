using City.DTO;
using Infrastructure.Abstraction;

namespace City.Abstractions;

    public interface ICityService : ICrudService<City, CreatingCityDTO, UpdatingCityDTO, CityDTO>
    {
    }

