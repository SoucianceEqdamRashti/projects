using BlazorApplicationInsights;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using MudBlazor;
using SkaneRegionalPlaces.App.Client.Shared;
using SkaneRegionalPlaces.App.Client.ViewModels;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace SkaneRegionalPlaces.App.Client.Pages
{
    public partial class RegionalPlaceOverview
    {
        [Inject]
        private ILogger<RegionalPlaceOverview> Logger { get; set; }

        [Inject]
        public RegionalPlaceOverviewViewModel RegionalPlaceOverviewViewModel { get; set; }

        [Inject]
        private IApplicationInsights AppInsights { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
       public  HttpClientInterceptor Interceptor { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }
        [CascadingParameter]
        public GlobalError Error { get; set; }

        private IEnumerable<RegionalPlace> pagedData;
        private MudTable<RegionalPlace> table;
        private int totalItems;
        private string searchString = null;
        IEnumerable<RegionalPlace> RegionalPlaces { get; set; }

        protected RegionalPlaceDetails RegionalPlaceDetails { get; set; }

        protected override async Task OnAfterRenderAsync(bool first)
        {
            Logger.LogInformation("First Render");
            Logger.LogInformation("Second Render");
            await AppInsights.TrackEvent("Souciance Event");
            await AppInsights.TrackPageViewPerformance(new PageViewPerformanceTelemetry()
            {
                Name = "AppPerformance",

            });
            await AppInsights.TrackPageView("Track-RegionalPlaceOverview");
            await AppInsights.TrackException(new Error() { Message = "Trying to get table data without a valid token", Name = "AuthenticationError" }, null, SeverityLevel.Critical);
            await AppInsights.Flush();
            throw new Exception("Break it");
        }
        protected override async Task OnInitializedAsync()
        {
            Interceptor.BeforeSend += Interceptor_BeforeSend;
            Interceptor.AfterSend += Interceptor_AfterSend;
            try
            {
               //await Task.Delay(4000);
                RegionalPlaces = await RegionalPlaceOverviewViewModel.LoadAllRegionalPlacesAsync();
                Logger.LogInformation("Table data received");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                Error.ProcessError(exception);
                exception.Redirect();
            }
            catch (Exception exception)
            {
                Error.ProcessError(exception);
            }
        }

        void Interceptor_BeforeSend(object sender, HttpClientInterceptorEventArgs e)
        {
            Console.WriteLine("Weather forecast - before send HTTP request.");
        }

        void Interceptor_AfterSend(object sender, HttpClientInterceptorEventArgs e)
        {
            Console.WriteLine("Weather forecast - after send HTTP request.");
        }

        public void Dispose()
        {
            Interceptor.BeforeSend -= Interceptor_BeforeSend;
            Interceptor.AfterSend -= Interceptor_AfterSend;
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<RegionalPlace>> ServerReload(TableState state)
        {

            IEnumerable<RegionalPlace> data = await RegionalPlaceOverviewViewModel.LoadAllRegionalPlacesAsync();

            data = data.Where(element =>
           {
               if (string.IsNullOrWhiteSpace(searchString))
                   return true;
               if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Location.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Latitude.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Longitude.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               if (element.Url.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                   return true;
               return false;
           }).ToArray();
            totalItems = data.Count();
            CultureInfo culture = new("sv-SE"); StringComparer.Create(culture, false);
            switch (state.SortLabel)
            {
                case "name_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Name);
                    break;
                case "location_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Location);
                    break;
                case "address_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Address);
                    break;
                case "url_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Url);
                    break;
                case "tags_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Tags);
                    break;
                case "description_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Description);
                    break;
                case "longitude_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Longitude);
                    break;
                case "latitude_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Latitude);
                    break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<RegionalPlace>() { TotalItems = totalItems, Items = pagedData };
        }
        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        protected async Task ShowSelectedRegionalPlaceDialogAsync(TableRowClickEventArgs<RegionalPlace> regionalPlaceSelectedRow)
        {
            var parameters = new DialogParameters { ["regionalPlace"] = regionalPlaceSelectedRow.Item };
            var dialog = DialogService.Show<RegionalPlaceDetails>($"Detaljer om {regionalPlaceSelectedRow.Item.Name}", parameters);
            await dialog.Result;
        }

    }
}
