using BlazorApplicationInsights;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using SkaneRegionalPlaces.App.Client.Services;
using SkaneRegionalPlaces.App.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace SkaneRegionalPlaces.App.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<IServerApiService, RegionalPlaceDataService>(
                (serviceProvider, client) =>
                {
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                    client.EnableIntercept(serviceProvider);
                })
                 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddLoadingBar();
            builder.UseLoadingBar();
            builder.Services.AddScoped<RegionalPlaceOverviewViewModel>();
            builder.Services.AddScoped<IEmailService, RegionalPlaceEmailService>();
            builder.Services.AddMudServices();
            builder.Services.AddMudBlazorDialog();
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://049ffe1b-ca17-4de9-b419-a2a53b39e30a/API.Access");
            });

            builder.Services.AddBlazorApplicationInsights(async applicationInsights =>
            {
                var telemetryItem = new TelemetryItem()
                {
                    Tags = new Dictionary<string, object>()
            {
                { "ai.cloud.role", "SPA" },
                { "ai.cloud.roleInstance", "Blazor Wasm Regional Platser i Skane" },
            }
                };

                await applicationInsights.AddTelemetryInitializer(telemetryItem);
            });

            await builder.Build().RunAsync();
        }
    }
}
