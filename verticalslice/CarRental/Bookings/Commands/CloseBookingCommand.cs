using BookingApi.Bookings.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Commands;
public record CloseBookingCommand(string BookingNumber, int EndMileage, DateTime EndDateBooking) : IRequest<IEvent>;
