﻿namespace BlazorApp.Models.Filter
{
    public class FilterParameters
    {
        public Guid? CountryId;
        public Guid? CityId;

        public Gender? Gender;
        public FamilyStatus? FamilyStatus;
        public ChildrenStatus? ChildrenStatus;
        public bool HasChildren;

        public IEnumerable<int> Range = new List<int> { 18, 65 };

        public int ItemsPerPage = 200;
        public int Page = 1;
    }
}
