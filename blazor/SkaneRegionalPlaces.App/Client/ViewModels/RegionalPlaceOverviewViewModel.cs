using Microsoft.AspNetCore.Components;
using SkaneRegionalPlaces.App.Client.Services;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.ViewModels
{
    public class RegionalPlaceOverviewViewModel
    {
        private IEnumerable<RegionalPlace> RegionalPlaces { get; set; }
        public bool IsLoading { get; set; }

        public bool HasLoaded { get; set; }
        
        public IServerApiService RegionalDataService { get; set; }

        public RegionalPlaceOverviewViewModel(IServerApiService _RegionalDataService)
        {
            RegionalDataService = _RegionalDataService;
        }

        public async Task<IEnumerable<RegionalPlace>> LoadAllRegionalPlacesAsync()
        {
            if (!HasLoaded)
            {


                IsLoading = true;
                RegionalPlaces = (await RegionalDataService.GetAllRegionalPlaces()).ToList();
                IsLoading = false;
                HasLoaded = true;
            }
            return RegionalPlaces;
        }
    }
}
