using BookingApi.DataAccess.Repository;
using BookingApi.DataAccess.Exceptions;
using BookingApi.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApi.DataAccess.Models.CategoryModel;

namespace BookingApi.DataAccess.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDataAccessRepository _repository;
        private VehicleModel? Vehicle { get; set; }
        public BookingService(IDataAccessRepository repository)
        {
            _repository = repository;
        }
        private async Task<VehicleModel> VehicleLookup(string registrationNumber)
        {
            var vehicleLookup = (await _repository.GetVehicleByRegistrationNumberAsync(registrationNumber));
            if (vehicleLookup?.Any() != true)
            {
                throw new VehicleNotFoundException(registrationNumber, $"Vehicle with registration number {registrationNumber} not found");
            }
            //a vehicle was found in the system
            Vehicle = vehicleLookup.First();
            return Vehicle;

        }
        public async Task<AddBookingResponseModel> CreateBookingAsync(AddBookingRequestModel addBooking)
        {
            //First we lookup to see if vehicle with the registration number is found
            VehicleModel vehicle = await VehicleLookup(addBooking.RegistrationNumber);
            //Then we see if customer with person number exists
            var customerLookup = (await _repository.GetCustomerByPersonNumberAsync(addBooking.PersonNumber));
            //We get the customer id of the existing or newly created customer
            int customerId = await GetCustomerIdAsync(addBooking.PersonNumber, customerLookup);
            //Generate a booking number to represent the booking
            var bookingNumber = GenerateBookingNumber();
            //Add the data to the rental table to hold the resevation
            await AddRentalAsync(vehicle, customerId, bookingNumber, addBooking.DateOfBooking);
            //Retun the booking number as a booking model object
            return new AddBookingResponseModel() { BookingNumber = bookingNumber };
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
        private async Task AddRentalAsync(VehicleModel vehicle, int customerId, string bookingNumber, DateTime startDateBooking)
        {
            RentalModel model = new()
            {
                CustomerId = customerId,
                VehicleId = vehicle.VehicleId,
                BookingNumber = bookingNumber,
                StartDate = startDateBooking,
                StartMileage = vehicle.Mileage
            };
            await _repository.AddBookingAsync(model);
        }

        private async Task<int> AddCustomerAsync(string personNumber)
        {
            //customer doesn't exist so add the customer and return the customer id
            CustomerModel customer = new() { PersonNumber = personNumber };
            var customerId = await _repository.AddCustomerAsync(customer);
            return customerId;
        }

        private static string GenerateBookingNumber()
        {            
            var ticks = new DateTime(2000, 1, 1).Ticks;
            var resultantTicks = DateTime.Now.Ticks - ticks;
            var bookingNumber = resultantTicks.ToString("x"); 
            return bookingNumber;
        }

        private async Task<VehicleCategories> GetVehicleCategoryAsync(int vehicleId)
        {
            var vehicleCategory = await _repository.GetVehicleCategoryByVehicleIdAsync(vehicleId);
            if (vehicleCategory?.Any() != true)
            {
                throw new VehicleNotFoundException(vehicleId.ToString(), $"Vehicle with id {vehicleId} not found");
            }
            return vehicleCategory.First();
        }


        public async Task<CloseBookingResponseModel> CloseBookingAsync(CloseBookingRequestModel closeBookingRequest)
        {
            //First we need to look up the booking number to see if it exists or not
            var bookingLookup = await _repository.GetBookingByBookingNumberAsync(closeBookingRequest.BookingNumber);
            if (bookingLookup?.Any() != true)
            {
                //Booking number not found
                throw new BookingNumberNotFoundException($"Booking with booking number {closeBookingRequest.BookingNumber} not found");
            }
            else
            {
                VehicleCategories bookedCarType = await GetVehicleCategoryAsync(bookingLookup.First().VehicleId);
               
                //Booking number found
                //We need to update the booking with the date, mileage and total price
                BookingPriceCalculator carRentalPrice = new(
                   bookedCarType, bookingLookup.First().StartDate, closeBookingRequest.EndDateBooking, 
                   bookingLookup.First().StartMileage, closeBookingRequest.EndMileage);
                decimal totalPrice = carRentalPrice.CalculateTotalPriceOfBooking();

                /**
                 * Create the rental model and include the end date
                 * Update the rental
                 * Update the vehicle mileage
                 */
                RentalModel rentalModel = new()
                {
                    BookingNumber = closeBookingRequest.BookingNumber,
                    EndMileage = closeBookingRequest.EndMileage,
                    EndDate = closeBookingRequest.EndDateBooking,
                    TotalPrice = totalPrice
                };
                VehicleModel vehicleToUpdate = new()
                {
                    Mileage = closeBookingRequest.EndMileage,
                    VehicleId = bookingLookup.First().VehicleId
                };
                await _repository.UpdateBookingAndVehicleAsync(rentalModel, vehicleToUpdate);                
                CloseBookingResponseModel closeBooking = new()
                {
                    BookingNumber = closeBookingRequest.BookingNumber,
                    TotalPrice = totalPrice
                };
                return closeBooking;
            }
        }
    }
}
