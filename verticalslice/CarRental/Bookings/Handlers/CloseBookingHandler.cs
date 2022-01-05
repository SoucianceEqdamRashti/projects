using BookingApi.Bookings.Commands;
using BookingApi.Bookings.Events;
using BookingApi.Bookings.Models;
using BookingApi.Bookings.Handlers;
using BookingApi.Bookings.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApi.Bookings.Models.CategoryModel;

namespace BookingApi.Bookings.Handlers
{
    public class CloseBookingHandler : IRequestHandler<CloseBookingCommand, IEvent>
    {
        public IDataAccessRepository _repo;
        public CloseBookingHandler(IDataAccessRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEvent> Handle(CloseBookingCommand request, CancellationToken cancellationToken)
        {
            //First we need to look up the booking number to see if it exists or not
            var bookingLookup = await _repo.GetBookingByBookingNumberAsync(request.BookingNumber);
            if (bookingLookup?.Any() != true)
            {
                //Booking number not found                
                return new BookingNumberNotFoundEvent();
            }
            else
            {
                var listOfVehicleCategories = await _repo.GetVehicleCategoryByVehicleIdAsync(bookingLookup.First().VehicleId);
                if (listOfVehicleCategories.Any() != true)
                    return new VehicleNotFoundEvent();
                VehicleCategories vehicleCategory = listOfVehicleCategories.First();
                //Booking number found
                //We need to update the booking with the date, mileage and total price
                BookingPriceCalculator carRentalPrice = new(
                   vehicleCategory, bookingLookup.First().StartDate, request.EndDateBooking,
                   bookingLookup.First().StartMileage, request.EndMileage);
                decimal totalPrice = carRentalPrice.CalculateTotalPriceOfBooking();

                /**
                 * Create the rental model and include the end date
                 * Update the rental
                 * Update the vehicle mileage
                 */
                RentalModel rentalModel = new()
                {
                    BookingNumber = request.BookingNumber,
                    EndMileage = request.EndMileage,
                    EndDate = request.EndDateBooking,
                    TotalPrice = totalPrice
                };
                GetAllVehiclesCommandResult vehicleToUpdate = new()
                {
                    Mileage = request.EndMileage,
                    VehicleId = bookingLookup.First().VehicleId
                };
                await _repo.UpdateBookingAndVehicleAsync(rentalModel, vehicleToUpdate);
                return new BookingClosedEvent(request.BookingNumber, totalPrice);                
            }
        }     
    }
}
