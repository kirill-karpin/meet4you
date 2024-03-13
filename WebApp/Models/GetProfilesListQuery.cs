using User;

namespace WebApp.Models
{
    public class GetProfilesListQuery
    {
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public bool? Gender { get; set; }
        public FamilyStatus? FamilyStatus { get; set; }
        public bool? HaveChildren { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }


    }
}