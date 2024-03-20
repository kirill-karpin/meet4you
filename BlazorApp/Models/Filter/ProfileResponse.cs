using System.Text.Json.Serialization;

namespace BlazorApp.Models.List
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }

        public UserRepsonse User { get; set; }
    }
}
