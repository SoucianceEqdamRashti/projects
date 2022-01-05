using BookingApi.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApi.Bookings.Models.CategoryModel;

namespace BookingApi.Bookings.Repository
{
    public interface IDataAccessRepository 
    {
        Task<IEnumerable<GetAllVehiclesCommandResult>> GetVehicleByRegistrationNumberAsync(string registrationNumber);
        Task<IEnumerable<CustomerModel>> GetCustomerByPersonNumberAsync(string personNumber);
        Task<int> AddCustomerAsync(CustomerModel customer);
        Task AddBookingAsync(RentalModel booking);        
        Task<IEnumerable<RentalModel>> GetBookingByBookingNumberAsync(string bookingNumber);
        Task UpdateBookingAndVehicleAsync(RentalModel booking, GetAllVehiclesCommandResult vehicle);        
        Task<IEnumerable<VehicleCategories>> GetVehicleCategoryByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<GetAllVehiclesCommandResult>> GetAllVehiclesAsync();
        Task<IEnumerable<RentalModel>> GetAllBookingsAsync();
    }
}
