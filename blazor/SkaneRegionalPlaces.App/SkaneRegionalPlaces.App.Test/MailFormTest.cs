using BlazorApplicationInsights;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using SkaneRegionalPlaces.App.Client.Services;
using SkaneRegionalPlaces.App.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;
using FluentAssertions;

namespace SkaneRegionalPlaces.App.Test
{
    public class MailFormTest
    {
        [Fact]
        public void TestFormValidation()
        {
            using var ctx = new TestContext();
            ctx.AddTestServices();
            var cut = ctx.RenderComponent<MailForm>();
            var form = cut.FindComponent<MudForm>().Instance;            
            form.IsValid.Should().Be(true);
            cut.Find("#MailSubmitButton").Click();
            form.IsValid.Should().Be(false);
            var errors = form.Errors;
            Assert.Equal("Ärende text får inte vara tomt", errors.Last()); //replae magic string with constant in a shared class?
        }
    }

    public static class TestContextExtensions
    {
        public static void AddTestServices(this Bunit.TestContext ctx)
        {
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddMudServices(options =>
            {
                options.SnackbarConfiguration.ShowTransitionDuration = 0;
                options.SnackbarConfiguration.HideTransitionDuration = 0;
            });
            ctx.Services.AddMudBlazorDialog();
            ctx.Services.AddScoped(sp => new HttpClient());
            ctx.Services.AddSingleton<IEmailService, RegionalPlaceEmailService>();
            ctx.Services.AddOptions();
            ctx.Services.AddHttpClient<PublicClient>(client => client.BaseAddress = new Uri("https://localhostdummy.com"));
            ctx.Services.AddBlazorApplicationInsights(async applicationInsights =>
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
        }
    }
}