using BookingApi.Bookings.Commands;
using BookingApi.Bookings.Exceptions;
using BookingApi.Bookings.Models;
using BookingApi.Bookings.Repository;
using MediatR;

namespace BookingApi.Bookings.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, AddBookingResponseModel>
    {
        private readonly IDataAccessRepository _repo;
        public CreateBookingHandler(IDataAccessRepository repo)
        {
            _repo = repo;
        }
        public async Task<AddBookingResponseModel> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            //First we lookup to see if vehicle with the registration number is found
            GetAllVehiclesCommandResult vehicle = await VehicleLookup(request.VehicleRegistrationNumber);
            //Then we see if customer with person number exists
            var customerLookup = (await _repo.GetCustomerByPersonNumberAsync(request.PersonNumber));
            //We get the customer id of the existing or newly created customer
            int customerId = await GetCustomerIdAsync(request.PersonNumber, customerLookup);
            //Generate a booking number to represent the booking
            var bookingNumber = GenerateBookingNumber();
            //Add the data to the rental table to hold the resevation
            await AddBookingAsync(vehicle, customerId, bookingNumber, request.DateOfBooking);
            //Retun the booking number as a booking model object
            return new AddBookingResponseModel() { BookingNumber = bookingNumber };
        }

        private async Task<GetAllVehiclesCommandResult> VehicleLookup(string registrationNumber)
        {
            var vehicleLookup = (await _repo.GetVehicleByRegistrationNumberAsync(registrationNumber));
            if (vehicleLookup?.Any() != true)
            {
                throw new VehicleNotFoundException(registrationNumber, $"Vehicle with registration number {registrationNumber} not found");
            }
            //a vehicle was found in the system
            return vehicleLookup.First();
        }

        private async Task<int> GetCustomerIdAsync(string personNumber, IEnumerable<CustomerModel> customerLookup)
        {
            if (customerLookup?.Any() != true)
            {
                //Customer does not exist so we add customer to the database
                return await AddCustomerAsync(personNumber);
            }
            else
            {
                //customer already exists so use existing customer id
                return customerLookup.First().CustomerId;
            }
        }

        private async Task<int> AddCustomerAsync(string personNumber)
        {
            //customer doesn't exist so add the customer and return the customer id
            CustomerModel customer = new() { PersonNumber = personNumber };
            var customerId = await _repo.AddCustomerAsync(customer);
            return customerId;
        }

        private static string GenerateBookingNumber()
        {
            var ticks = new DateTime(2000, 1, 1).Ticks;
            var resultantTicks = DateTime.Now.Ticks - ticks;
            var bookingNumber = resultantTicks.ToString("x");
            return bookingNumber;
        }

        private async Task AddBookingAsync(GetAllVehiclesCommandResult vehicle, int customerId, string bookingNumber, DateTime startDateBooking)
        {
            RentalModel model = new()
            {
                CustomerId = customerId,
                VehicleId = vehicle.VehicleId,
                BookingNumber = bookingNumber,
                StartDate = startDateBooking,
                StartMileage = vehicle.Mileage
            };
            await _repo.AddBookingAsync(model);
        }
    }
}
