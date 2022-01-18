using Microsoft.AspNetCore.Components;
using MudBlazor;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Shared
{
    public partial class RegionalPlaceDetails
    {
       public bool ShowDialog { get; set; }

        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public RegionalPlace RegionalPlace { get; set; } = new RegionalPlace();

        public void Close()
        {
            MudDialog.Close();
        }
      
    }
}
