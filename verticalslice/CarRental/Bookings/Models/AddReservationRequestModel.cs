using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Models
{
    public class AddBookingRequestModel
    {
        public string PersonNumber { get; set; }
        public string RegistrationNumber { get; set; }  
        public DateTime DateOfBooking { get; set; } 
    }
    
}
