using BookingApi.Bookings.Repository;
using BookingApi.Bookings.Exceptions;
using BookingApi.Bookings.Models;
using BookingApi.Bookings.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingApi.Tests.DataAccess.Services
{
    public class BookingServiceTests
    {
        private readonly IDataAccessRepository carRentalRepository = new FakeBookingRespository();        

        [Fact]
        public async Task TestThatCustomerAbleToBookVehicle() {
            BookingService booking = new(carRentalRepository);
            var bookCar = new AddBookingRequestModel()
            {
                PersonNumber = "19811103-0521",
                RegistrationNumber = "ECG 96L",
                DateOfBooking  = DateTime.Now,
            };
            var response = await booking.CreateBookingAsync(bookCar);
            Assert.False(String.IsNullOrEmpty(response.BookingNumber));            
        }

        [Fact]
        public async Task TestThatCustomerAbleToReturnVehicle()
        {
            BookingService booking = new(carRentalRepository);
            var closeBooking = new CloseBookingRequestModel()
            {
                BookingNumber = "123",
                EndDateBooking = DateTime.Now,
                EndMileage = 5000
            };
            var closeBookingResponse = await booking.CloseBookingAsync(closeBooking);
            Assert.True(closeBookingResponse.BookingNumber.Equals("123"));
            Assert.True(closeBookingResponse.TotalPrice > 0);
        }

        [Fact]
        public async Task TestThatCustomerWithoutValidBookingNumberThrowsException()
        {
            BookingService booking = new(carRentalRepository);
            var closeBooking = new CloseBookingRequestModel()
            {
                BookingNumber = "DOESNOTEXIST",
                EndDateBooking = DateTime.Now,
                EndMileage = 5000
            };            
            var exception = await Assert.ThrowsAsync<BookingNumberNotFoundException>(() => 
            booking.CloseBookingAsync(closeBooking));
            Assert.Contains("DOESNOTEXIST", exception.Message);
        }


        [Fact]
        public async Task TestThatVehicleNotInSystemThrowsException() {
            BookingService booking = new(carRentalRepository);
            var bookCar = new AddBookingRequestModel()
            {
                PersonNumber = "19811103-0521",
                RegistrationNumber = "DOESNOTEXIST",
                DateOfBooking = DateTime.Now,
            };            
            var exception = await Assert.ThrowsAsync<VehicleNotFoundException>(() => booking.CreateBookingAsync(bookCar));
            Assert.Contains("DOESNOTEXIST", exception.Message);
        }
    }
}
