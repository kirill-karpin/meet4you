using User;

namespace WebApp.Models
{
    public class ListFilterModel
    {
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public bool? Gender { get; set; }
        public FamilyStatus? FamilyStatus { get; set; }
        public bool? HaveChildren { get; set; }

    }
}