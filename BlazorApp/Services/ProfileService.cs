

namespace BlazorApp.Services
{
    public class ProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetListProfile()
        {
            var data = await _httpClient.GetStringAsync("https://localhost:7172/api/profile/list");
            return data;
        }
    }
}
