

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

        public async Task<object[]> GetListProfile()
        {
            var data = await _httpClient.GetFromJsonAsync<object[]>("https://localhost:7172/api/profile/list");
            return data;
        }
    }
}
