using Microsoft.AspNetCore.Components;
using MudBlazor;
using SkaneRegionalPlaces.App.Client.Services;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Pages
{
    public partial class AddRegionalPlace
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IServerApiService RegionalDataService { get; set; }

        private MudForm RegionalPlaceForm { get; set; }
        private RegionalPlace RegionalPlace { get; set; } = new();

        private async Task Submit()
        {
            //  await RegionalPlaceForm.Validate();

            //if (RegionalPlaceForm.IsValid)
            // {
            var sent = true;// await RegionalDataService.AddRegionalPlace(RegionalPlace);
                if (sent)
                {
                    Snackbar.Add("Din nya plats har lagts till!");
                    Reset();
                }
                else
                {
                    Snackbar.Add("Platsen kunde inte läggas till. Prova igen!");
                }
           // }
        }

        private void Reset()
        {
            RegionalPlace.Name = "";
            RegionalPlace.Longitude = "";
            RegionalPlace.Latitude = "";
            RegionalPlace.Description = "";
            RegionalPlace.Location = "";
            RegionalPlace.Url = "";
            RegionalPlace.Tags = "";
            RegionalPlace.Address = "";

        }
    }
}
