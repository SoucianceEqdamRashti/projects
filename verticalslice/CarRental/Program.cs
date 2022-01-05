using BookingApi.Bookings.Validators;
using BookingApi.Bookings.Repository;
using BookingApi.Bookings.Exceptions;

using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
    })
    .AddFluentValidation(x =>
    {
        x.DisableDataAnnotationsValidation = true;
        x.RegisterValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();
    });

builder.Configuration.AddEnvironmentVariables();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbConnectionFactory, SqlServerConnectionFactory>();
builder.Services.AddScoped<IDataAccessRepository, BookingRepository>();
//builder.Services.AddScoped<IBookingService, BookingService>();
//builder.Services.AddMediatR(typeof(this).Assembly);
builder.Services.AddProblemDetails(setup =>
{
    setup.IncludeExceptionDetails = (context, ex) =>
    {
        var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
        return environment.IsDevelopment();
    };
    setup.Map<VehicleNotFoundException>(exception => new ProblemDetails()
    {
        Title = "Vehicle not found",
        Detail = "Vehicle not found or registration number is invalid",
        Status = StatusCodes.Status404NotFound,
        Type = exception.GetType().Name,
        Instance = exception.Message
    });
    setup.Map<AddBookingToDatabaseException>(exception => new ProblemDetails()
    {
        Title = "Unable to create booking",
        Detail = "Error inserting booking in database",
        Status = StatusCodes.Status500InternalServerError,
        Type = exception.GetType().Name,
        Instance = exception.Message
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
        c.ConfigObject = new ConfigObject()
        {
            ShowCommonExtensions = true
        };
    });
}
app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
