using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Models
{
    public class CloseBookingResponseModel
    {
        public string BookingNumber { get; set; }   
        public decimal TotalPrice { get; set; }
    }
}
