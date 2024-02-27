using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.UserLocation.DTO
{
    public class UserLocationDTO
    {
        public Guid UserId { get; set; }
        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
