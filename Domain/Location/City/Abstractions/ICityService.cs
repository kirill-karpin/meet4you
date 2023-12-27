﻿using Infrastructure.Abstraction;
using Location.City.DTO;


namespace Location.City.Abstractions;

public interface ICityService : ICrudService<City, CreatingCityDTO, UpdatingCityDTO, CityDTO>
{
}