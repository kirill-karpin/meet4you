using BlazorApp.Models.List;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Models.Filter
{
    public class FilterParameters
    {
        public Guid? CountryId;
        public Guid? CityId;

        public Gender? Gender;
        public FamilyStatus? FamilyStatus;
        public bool HasChildren;

        public IEnumerable<int> Range = new List<int> { 18, 65 };

        public int ItemsPerPage = 30;
        public int Page = 1;
    }
}
