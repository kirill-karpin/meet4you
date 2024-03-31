using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp.Models.List
{
    public class UserRepsonse
    {
        public Guid Id { get; set; }

        public bool Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Location Location { get; set; }
    }

    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
