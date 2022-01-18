using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Services
{
    public class RegionalPlaceDataService : IServerApiService
    {
        private readonly HttpClient _httpClient;

        public RegionalPlaceDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<RegionalPlace>> GetAllRegionalPlaces()
        {

            return await _httpClient.GetFromJsonAsync<IEnumerable<RegionalPlace>>("/places");
        }

        public async Task<bool> AddRegionalPlace(RegionalPlace place)
        {
           var response = await _httpClient.PostAsJsonAsync("/places", place);
            return response.IsSuccessStatusCode;
        }
    }
}
