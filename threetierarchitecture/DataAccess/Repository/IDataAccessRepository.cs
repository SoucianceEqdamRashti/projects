using BookingApi.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApi.DataAccess.Models.CategoryModel;

namespace BookingApi.DataAccess.Repository
{
    public interface IDataAccessRepository 
    {
        Task<IEnumerable<VehicleModel>> GetVehicleByRegistrationNumberAsync(string registrationNumber);
        Task<IEnumerable<CustomerModel>> GetCustomerByPersonNumberAsync(string personNumber);
        Task<int> AddCustomerAsync(CustomerModel customer);
        Task AddBookingAsync(RentalModel booking);        
        Task<IEnumerable<RentalModel>> GetBookingByBookingNumberAsync(string bookingNumber);
        Task UpdateBookingAndVehicleAsync(RentalModel booking, VehicleModel vehicle);        
        Task<IEnumerable<VehicleCategories>> GetVehicleCategoryByVehicleIdAsync(int vehicleId);
    }
}
