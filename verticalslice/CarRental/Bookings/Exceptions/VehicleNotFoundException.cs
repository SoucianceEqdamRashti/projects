using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Exceptions
{
    public class VehicleNotFoundException : Exception 
    {
        public VehicleNotFoundException(string registrationNumber, string message) : base(message)
        {

        }
    }
}
