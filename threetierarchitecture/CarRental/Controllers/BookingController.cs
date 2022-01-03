using BookingApi.Dtos.Requests;
using BookingApi.Dtos.Responses;
using BookingApi.DataAccess.Repository;
using BookingApi.DataAccess.Models;
using BookingApi.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;

namespace BookingApi.Controllers
{

    [ApiController]
    [Route("api/bookings")]
  
    public class BookingController : ControllerBase
    {       
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _carRentalService;

        public BookingController(ILogger<BookingController> logger, IBookingService carRentalService)
        {
            _logger = logger;
            _carRentalService = carRentalService;
        }

        [Route("cars/{registrationNumber}")]
        [HttpPost]
        [Produces("application/json")]        
        public async Task<ActionResult<CreateBookingResponseDto>> CreateBooking(
            [FromRoute][Required] string registrationNumber, [FromBody] CreateBookingRequestDto customer)
        {
            _logger.LogInformation("Received request to book vehicle for registration number {registratioNumber}", registrationNumber);
            AddBookingRequestModel addBooking = new();
            addBooking.PersonNumber = customer.PersonNumber;
            addBooking.RegistrationNumber = registrationNumber;
            addBooking.DateOfBooking = customer.DateOfBooking;

            AddBookingResponseModel bookingModel = await _carRentalService.CreateBookingAsync(addBooking);
            CreateBookingResponseDto booking = new()
            {
                BookingNumber = bookingModel.BookingNumber,
            };
            var bookingNumber = bookingModel.BookingNumber;
            _logger.LogInformation("Returning booking number {bookingNumber}", bookingNumber);
            return booking;
        }

        [Route("{bookingNumber}")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CloseBookingResponseDto>> CloseBooking([FromRoute][Required] string bookingNumber, [FromBody] CloseBookingRequestDto booking)
        {
            _logger.LogInformation("Received request for booking number {bookingNumber}", bookingNumber);
            CloseBookingRequestModel closeBooking = new();
            closeBooking.BookingNumber = bookingNumber;
            closeBooking.EndMileage = booking.EndMileage;
            closeBooking.EndDateBooking = booking.EndDateBooking;
            CloseBookingResponseModel closeBookingResponseModel = await _carRentalService.CloseBookingAsync(closeBooking);
            CloseBookingResponseDto closeBookingResponse = new();
            closeBookingResponse.BookingNumber = closeBookingResponseModel.BookingNumber;
            closeBookingResponse.TotalPrice = closeBookingResponseModel.TotalPrice;
            var totalPrice = closeBookingResponse.TotalPrice;
            _logger.LogInformation("Returning total price {bookingNumber}", totalPrice);
            return closeBookingResponse;
        }
    }
}