using Newtonsoft.Json;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Services
{
    public class PublicClient
    {
        public HttpClient Client { get; }

        public PublicClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<bool> PostEmailAsync(Contact contact)
        {        
            var response = await Client.PostAsJsonAsync("/email", contact);
            return response.IsSuccessStatusCode;
        }
    }
}
