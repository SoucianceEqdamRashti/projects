using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Models
{
    public class RentalModel
    {
        public int RentalId { get; set; } 
        public int CustomerId { get; set; } 
        public int VehicleId { get; set; }
        public int StartMileage { get; set; }   
        public int EndMileage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public decimal TotalPrice { get; set; } 
        public string BookingNumber { get; set; }   

    }
}
