using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Models
{
    public class CloseBookingRequestModel
    {        
        public int EndMileage { get; set; }
        public string BookingNumber { get; set; }   
        public DateTime EndDateBooking { get; set; }    
    }
}
