using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Exceptions
{
    public class BookingNumberNotFoundException : Exception
    {
        public BookingNumberNotFoundException(string message) : base(message)   
        {

        }
    }
}
