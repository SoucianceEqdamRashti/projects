using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Bookings.Models
{
    public class GetAllVehiclesCommandResult
    {
        public int VehicleId { get; set; }
        public int Mileage { get; set; }
        public string RegistrationNumber { get; set; }
        public int CategoryId { get; set; }
    }
}
