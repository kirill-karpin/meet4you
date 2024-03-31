using BlazorApp.Models.List;
using System.Net;
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

        public async Task<ProfileResponse[]> GetListProfile(Guid? countryId, Guid? cityId, bool? gender, int? status, bool haveChildren, int ageFrom, int ageTo)
        {
            Dictionary<string, string> dic = new()
            {
                { "itemsPerPage","10" },
                { "page", "1" }
            };
            if (countryId.HasValue)
                dic.Add("countryId", countryId.Value.ToString());
            if (cityId.HasValue)
                dic.Add("cityId", cityId.Value.ToString());
            if (gender.HasValue)
                dic.Add("gender", gender.Value.ToString());
            if (status.HasValue)
                dic.Add("familyStatus", status.Value.ToString());

            dic.Add("haveChildren", haveChildren.ToString());
            dic.Add("ageFrom", ageFrom.ToString());
            dic.Add("ageTo", ageTo.ToString());
            
            var queryString = string.Join("&", dic.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}").ToArray());
            string request= $"https://localhost:7172/api/profile/list?{queryString}";

            var data = await _httpClient.GetFromJsonAsync<ProfileResponse[]>(request);
            return data;
        }
        
        public async Task<ProfileResponse[]> GetListProfiles()
        {
            string query = $"https://localhost:7172/api/profile/list?itemsPerPage=10&page=1";
            var data = await _httpClient.GetFromJsonAsync<ProfileResponse[]>(query);
            return data;
        }
    }
}
