using Newtonsoft.Json;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Contact contact);
    }
    public class RegionalPlaceEmailService : IEmailService
    {
        private readonly PublicClient _httpClient;

        public RegionalPlaceEmailService(PublicClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendEmail(Contact contact)
        {
          
                return await _httpClient.PostEmailAsync(contact);
              
                
        }
    }
}
