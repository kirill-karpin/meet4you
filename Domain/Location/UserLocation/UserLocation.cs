﻿using Entities.Abstractions;

namespace Location.UserLocation
{
    public class UserLocation : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }
    }
}