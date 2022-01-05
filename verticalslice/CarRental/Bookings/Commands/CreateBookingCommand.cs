using BookingApi.Bookings.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Commands;
public record CreateBookingCommand(string VehicleRegistrationNumber, string PersonNumber, DateTime DateOfBooking) : IRequest<AddBookingResponseModel>;

