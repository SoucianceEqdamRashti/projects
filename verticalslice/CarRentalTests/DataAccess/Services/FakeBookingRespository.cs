using BookingApi.Bookings.Repository;
using BookingApi.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Tests.DataAccess.Services
{
    public class FakeBookingRespository : IDataAccessRepository
    {
        public Task<int> AddCustomerAsync(CustomerModel customer)
        {
            return Task.FromResult(1);
        }

        public Task AddBookingAsync(RentalModel booking)
        {
           return Task.FromResult(0);
        }

        public Task<IEnumerable<CustomerModel>> GetCustomerByPersonNumberAsync(string personNumber)
        {
            CustomerModel customer = new()
            {
                CustomerCreationDate = DateTime.Now.ToString(),
                CustomerId = 123,
                PersonNumber = personNumber
            };
            IEnumerable<CustomerModel> listOfCustomersFromRepository = new List<CustomerModel> { customer };
            return Task.FromResult(listOfCustomersFromRepository);
        }

        public Task<IEnumerable<RentalModel>> GetBookingByBookingNumberAsync(string bookingNumber)
        {
            if (bookingNumber.Equals("DOESNOTEXIST"))
                return Task.FromResult(Enumerable.Empty<RentalModel>());
            RentalModel booking = new()
            {
                BookingNumber = bookingNumber,
                CustomerId = 123,
                StartDate = DateTime.Now.AddDays(-2),
                StartMileage = 1000,
                VehicleId = 123
            };
            IEnumerable<RentalModel> ListOfBookingsFromRepository = new List<RentalModel> { booking };
            return Task.FromResult(ListOfBookingsFromRepository);
        }

        public Task<IEnumerable<GetAllVehiclesCommandResult>> GetVehicleByRegistrationNumberAsync(string registrationNumber)
        {
            if (registrationNumber.Equals("DOESNOTEXIST"))
                return Task.FromResult(Enumerable.Empty<GetAllVehiclesCommandResult>());
            GetAllVehiclesCommandResult vehicle = new()
            {
               VehicleId = 123,
               RegistrationNumber = registrationNumber,
               CategoryId = 1,
               Mileage = 500
            };
            IEnumerable<GetAllVehiclesCommandResult> ListOfVehiclesFromRepository = new List<GetAllVehiclesCommandResult> { vehicle };
            return Task.FromResult(ListOfVehiclesFromRepository);
        }

        public Task<IEnumerable<CategoryModel.VehicleCategories>> GetVehicleCategoryByVehicleIdAsync(int vehicleId)
        {           
            IEnumerable<CategoryModel.VehicleCategories> vehicleCategoryFromRepository = new List<CategoryModel.VehicleCategories> 
            {CategoryModel.VehicleCategories.SMALLCAR  };
            return Task.FromResult(vehicleCategoryFromRepository);
        }

        public Task UpdateBookingAndVehicleAsync(RentalModel booking, GetAllVehiclesCommandResult vehicle)
        {
            return Task.FromResult(1);
        }
    }
}
