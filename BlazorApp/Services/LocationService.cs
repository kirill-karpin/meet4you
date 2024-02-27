using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CountryResponse[]> GetCountries()
        {
            var data = await _httpClient.GetFromJsonAsync<CountryResponse[]>("https://localhost:7172/country/get-countries");
            return data;
        }

        public async Task<CityResponse[]> GetCitiesByCountryId(Guid id)
        {
            var data = await _httpClient.GetFromJsonAsync<CityResponse[]>($"https://localhost:7172/city/get-cities/{id}");
            return data;
        }
    }
}
