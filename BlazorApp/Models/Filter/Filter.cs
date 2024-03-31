using BlazorApp.Models.List;

namespace BlazorApp.Models.Filter
{
    public class Filter
    {
        public Gender? Gender;
        public FamilyStatus? FamilyStatus;
        public bool HasChildren;

        public IEnumerable<int> Range = new int[] { 18, 65 };
    }
}
