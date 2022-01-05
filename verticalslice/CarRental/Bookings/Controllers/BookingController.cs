
using BookingApi.Bookings.Dtos.Requests;
using BookingApi.Bookings.Dtos.Responses;
using BookingApi.Bookings.Repository;
using BookingApi.Bookings.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using MediatR;
using BookingApi.Bookings.Queries;
using BookingApi.Bookings.Commands;
using BookingApi.Bookings.Events;

namespace BookingApi.Bookings.Controllers
{

    [ApiController]
    [Route("api/bookings")]

    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMediator _mediator;

        public BookingController(ILogger<BookingController> logger, IMediator mediator)
        {
            _logger = logger;            
            _mediator = mediator;
        }

        [Route("vehicles/{registrationNumber}")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CreateBookingResponseDto>> CreateBooking(
            [FromRoute][Required] string registrationNumber, [FromBody] CreateBookingRequestDto customer)
        {
            _logger.LogInformation("Received request to book vehicle for registration number {registrationNumber}", registrationNumber);
            var bookingCreated = await _mediator.Send(new CreateBookingCommand(registrationNumber, customer.PersonNumber, customer.DateOfBooking));
            CreateBookingResponseDto booking = new()
            {
                BookingNumber = bookingCreated.BookingNumber,
            };
            var bookingNumber = bookingCreated.BookingNumber;
            _logger.LogInformation("Returning booking number {bookingNumber}", bookingNumber);
            return booking;
        }

        [Route("{bookingNumber}")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CloseBookingResponseDto>> CloseBooking([FromRoute][Required] string bookingNumber, [FromBody] CloseBookingRequestDto booking)
        {
            _logger.LogInformation("Received request for booking number {bookingNumber}", bookingNumber);
            var @event = await _mediator.Send(new CloseBookingCommand(bookingNumber, booking.EndMileage, booking.EndDateBooking));
            return @event switch
            {
                BookingNumberNotFoundEvent => NotFound(),
                VehicleNotFoundEvent => StatusCode(StatusCodes.Status500InternalServerError, "vehicle not found"),
                BookingClosedEvent => Ok(),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<RentalModel>>> GetAllBookings()
        {
            var allBookings = await _mediator.Send(new GetAllBookingsQuery());
            return StatusCode(StatusCodes.Status200OK, allBookings);
        }
    }
}