using BookingApi.Dtos.Requests;
using BookingApi.Dtos.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingApi.Tests.IntegrationTests;

public class BookingControllerTests
{
    private readonly IConfiguration _configuration;

    public BookingControllerTests()
    {
        /**
         * Not sure if its a bug in dotnet 6 but could not get the webhostBuilder to override the default appsettings.json
         * with my custom appsettings.json in my test project.
         * Hence forced the override via environment variable instead.
         */
        Environment.SetEnvironmentVariable("ConnectionStrings:Default", "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VehicleBookingsTest;Integrated Security=True");
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())            
            .AddEnvironmentVariables()
            .Build();
    }
    [Fact]
    public async Task TestThatBookingCarReturnsBookingNumber()
    {
        
        using var app = new WebApplicationFactory<Program>();
        app.WithWebHostBuilder(webHostBuilder =>
        {            
            webHostBuilder.UseConfiguration(_configuration);
            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());            
        });
        
        using var client = app.CreateClient();
        CreateBookingRequestDto createBooking = new()
        {
            DateOfBooking = DateTime.Now.AddDays(-2),
            PersonNumber = "19851103-0521"
        };
        HttpRequestMessage requestMessage = new(HttpMethod.Post, "/api/bookings/cars/MLB%2035H")
        {
            Content = new StringContent(JsonConvert.SerializeObject(createBooking), Encoding.UTF8)
        };
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var httpResponse = await client.SendAsync(requestMessage);
        var httpResponseMessage = await httpResponse.Content.ReadAsStringAsync();   
        var responseMessage = JsonConvert.DeserializeObject<CreateBookingResponseDto>(httpResponseMessage);
        Assert.NotNull(responseMessage);
        Assert.True(String.IsNullOrEmpty(responseMessage.BookingNumber) == false);
    }

    [Fact]
    public async Task TestThatWhenCustomerClosesBookingThenTotalPriceIsReturned()
    {
        using var app = new WebApplicationFactory<Program>();
        app.WithWebHostBuilder(webHostBuilder =>
        {
            webHostBuilder.UseConfiguration(_configuration);
            webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory());
        });

        using var client = app.CreateClient();

        CreateBookingRequestDto createBooking = new()
        {
            DateOfBooking = DateTime.Now.AddDays(-2),
            PersonNumber = "19851103-0521"
        };
        HttpRequestMessage httpRequestBooking = new(HttpMethod.Post, "/api/bookings/cars/MLB%2035H")
        {
            Content = new StringContent(JsonConvert.SerializeObject(createBooking), Encoding.UTF8)
        };
        httpRequestBooking.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var requestBookingHttpResponse = await client.SendAsync(httpRequestBooking);
        var requestBookingHttpResponseMessage = await requestBookingHttpResponse.Content.ReadAsStringAsync();
        var requestBookingResponseBody = JsonConvert.DeserializeObject<CreateBookingResponseDto>(requestBookingHttpResponseMessage);
        Assert.NotNull(requestBookingResponseBody);
        var bookingNumber = requestBookingResponseBody.BookingNumber;

        CloseBookingRequestDto closeBooking = new()
        {
            EndDateBooking = DateTime.Now,
            EndMileage = 5000
        };
        HttpRequestMessage httpRequestCloseBooking = new(HttpMethod.Post, String.Concat("/api/bookings/" + bookingNumber))
        {
            Content = new StringContent(JsonConvert.SerializeObject(closeBooking), Encoding.UTF8)
        };
        httpRequestCloseBooking.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var requestCloseBookingHttpResponse = await client.SendAsync(httpRequestCloseBooking);
        var requestCloseBookingHttpResponseMessage = await requestCloseBookingHttpResponse.Content.ReadAsStringAsync();
        var requestCloseBookingHttpResponseBody = JsonConvert.DeserializeObject<CloseBookingResponseDto>(requestCloseBookingHttpResponseMessage);
        Assert.NotNull(requestCloseBookingHttpResponseBody);
        Assert.True(requestCloseBookingHttpResponseBody.BookingNumber.Equals(bookingNumber));
        Assert.True(requestCloseBookingHttpResponseBody.TotalPrice > 0);
    }
}