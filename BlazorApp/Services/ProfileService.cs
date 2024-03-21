using BlazorApp.Models.List;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class ProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<ProfileResponse[]> GetListProfile(bool gender, int status, bool children, int ageFrom, int ageTo)
        {
            string query = $"https://localhost:7172/api/profile/list?Gender={gender}&FamilyStatus={status}&HaveChildren={children}&AgeFrom={ageFrom}&AgeTo={ageTo}&itemsPerPage=10&page=1";
            var data = await _httpClient.GetFromJsonAsync<ProfileResponse[]>(query);
            return data;
        }
    }
}
