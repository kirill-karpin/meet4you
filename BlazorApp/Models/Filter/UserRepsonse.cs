using System.Threading.Tasks.Dataflow;

namespace BlazorApp.Models.Filter
{
    public class UserRepsonse
    {
            public Guid Id { get; set; }

            public bool Gender { get; set; }

            public DateTime DateOfBirth { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Surname { get; set; }

            public string About { get; set; }

            public string LookingFor { get; set; }

            public FamilyStatus FamilyStatus { get; set; }

            public bool HaveChildren { get; set; }

            public string Login { get; set; }

            public string UserName { get; set; }

            public int Age { get; set; }

            public Location Location { get; set; }

            public string ShortName()
            {
                string[] abc = {
                    FirstName, LastName
                };
                return String.Join(" ", abc);
            }
            
            public string ImageId { get; set; }
    }

    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
