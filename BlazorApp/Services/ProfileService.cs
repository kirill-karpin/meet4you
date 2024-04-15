using BlazorApp.Models.Filter;
using System.Net;
using System.Net.Http.Json;
using Common;
using User.Dto;

namespace BlazorApp.Services
{
    public class ProfileService
    {
        private readonly HttpClient _httpClient;
        private object? _getUserImage;
        public Profile? Profile { get; set; }

        public UserDto? User
        {
            get
            {
                return Profile?.User;
            }
        }

        public string? ImageId
        {
            get
            {
                return Profile?.User.ImageId;
            }
            
        }

        public ProfileService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        
        

        public async Task<Profile?> Me()
        {
            var data = await _httpClient.GetFromJsonAsync<Profile>($"https://localhost:7172/api/profile/me/");
            return data;
        }

        public async Task<ProfileResponse[]> GetListProfile(FilterParameters filterParameters)
        {
            if (filterParameters==null)  
                filterParameters = new FilterParameters();

            Dictionary<string, string> dic = new()
            {
                { "itemsPerPage",filterParameters.ItemsPerPage.ToString()},
                { "page", filterParameters.Page.ToString() }
            };
            if (filterParameters.CountryId.HasValue)
                dic.Add("countryId", filterParameters.CountryId.Value.ToString());
            if (filterParameters.CityId.HasValue)
                dic.Add("cityId", filterParameters.CityId.Value.ToString());
            if (filterParameters.Gender.HasValue)
                dic.Add("gender", (filterParameters.Gender.Value== Gender.Мужчина ? true : false).ToString());
            if (filterParameters.FamilyStatus.HasValue)
                dic.Add("familyStatus", filterParameters.FamilyStatus.Value.ToString());
            if (filterParameters.ChildrenStatus.HasValue)
                dic.Add("haveChildren", (filterParameters.ChildrenStatus == ChildrenStatus.Нет ? false : true).ToString());

            dic.Add("ageFrom", filterParameters.Range.First().ToString());
            dic.Add("ageTo", filterParameters.Range.Last().ToString());
            
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
