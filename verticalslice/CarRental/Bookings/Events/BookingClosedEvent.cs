using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Events;
public record BookingClosedEvent(string BookingNumber, decimal TotalPrice) : IEvent;
