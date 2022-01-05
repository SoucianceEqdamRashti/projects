using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Exceptions
{
    public class UpdateVehicleInDatabaseException : Exception 
    {
        public UpdateVehicleInDatabaseException(string message, Exception inner): base(message, inner)
        {

        }
    }
}
