using BookingApi.Bookings.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookingApi.Bookings.Models.CategoryModel;

namespace BookingApi.Tests.DataAccess.Services
{
    public class BookingPriceCalculatorTests
    {
       
        [Theory]
        [InlineData(0, 1000, 1000, 0.00)]
        [InlineData(-1, 1000, 1000, 500.00)]
        [InlineData(-2, 1000, 1000, 1000.00)]
        [InlineData(-3, 1000, 1000, 1500.00)]
        [InlineData(-4, 1000, 1000, 2000.00)]
        [InlineData(-5, 1000, 1000, 2500.00)]
        [InlineData(-6, 1000, 1000, 3000.00)]
        [InlineData(-7, 1000, 1000, 3500.00)]
        [InlineData(-8, 1000, 1000, 4000.00)]
        [InlineData(-9, 1000, 1000, 4500.00)]
        [InlineData(-10, 1000, 1000,5000.00)]
        [InlineData(-1.5, 1000, 1000, 750.00)]
        [InlineData(-2.5, 1000, 1000, 1250.00)]
        [InlineData(-3.5, 1000, 1000, 1750.00)]
        public void CalculateTotalPriceOfSmallCar(double daysToAddForStartingDate, int startMileageData, int endMileageData, decimal expectedTotalPrice)
        {
            VehicleCategories type = VehicleCategories.SMALLCAR;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddDays(daysToAddForStartingDate);
            int startMileage = startMileageData;
            int endMileage = endMileageData;
            BookingPriceCalculator carRentalPriceCalculator = new(type, startDate, endDate, startMileage, endMileage);
            decimal totalPrice = carRentalPriceCalculator.CalculateTotalPriceOfBooking();
            decimal expected = expectedTotalPrice;
            Assert.Equal(expected, totalPrice);
        }

        [Theory]
        [InlineData(0, 1000, 1000, 0.00)]
        [InlineData(-1, 1000, 1050, 755.00)]
        [InlineData(-2, 1000, 1100, 1510.00)]
        [InlineData(-3, 1000, 1150, 2265.00)]
        [InlineData(-4, 1000, 1250, 3125.00)]
        [InlineData(-5, 1000, 1300, 3880.00)]
        [InlineData(-6, 1000, 1225, 4372.50)]
        [InlineData(-7, 1000, 1110, 4781.00)]
        [InlineData(-8, 1000, 2200, 7720.00)]
        [InlineData(-9, 1000, 1300, 6480.00)]
        [InlineData(-10, 1000, 1150, 6815.00)]
        [InlineData(-1.5, 1000, 1025, 1027.50)]
        [InlineData(-2.5, 1000, 1021, 1669.10)]
        [InlineData(-3.5, 1000, 1030, 2338.00)]
        public void CalculateTotalPriceOfKombiCar(double daysToAddForStartingDate, int startMileageData, int endMileageData, decimal expectedTotalPrice)
        {
            VehicleCategories type = VehicleCategories.MEDIUMCAR;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddDays(daysToAddForStartingDate);
            int startMileage = startMileageData;
            int endMileage = endMileageData;

            BookingPriceCalculator carRentalPriceCalculator = new(type, startDate, endDate, startMileage, endMileage);
            decimal totalPrice = carRentalPriceCalculator.CalculateTotalPriceOfBooking();
            decimal expected = expectedTotalPrice;
            Assert.Equal(expected, totalPrice);
        }

        [Theory]
        [InlineData(0, 1000, 1000, 0.00)]
        [InlineData(-1, 1000, 1050, 907.50)]
        [InlineData(-2, 1000, 1100, 1815.00)]
        [InlineData(-3, 1000, 1150, 2722.50)]
        [InlineData(-4, 1000, 1250, 3787.50)]
        [InlineData(-5, 1000, 1300, 4695.00)]
        [InlineData(-6, 1000, 1225, 5208.75)]
        [InlineData(-7, 1000, 1110, 5596.50)]
        [InlineData(-8, 1000, 2200, 9780.00)]
        [InlineData(-9, 1000, 1300, 7695.00)]
        [InlineData(-10, 1000, 1150, 7972.5)]
        [InlineData(-1.5, 1000, 1025, 1203.75)]
        [InlineData(-2.5, 1000, 1021, 1941.15)]
        [InlineData(-3.5, 1000, 1030, 2719.5)]
        public void CalculateTotalPriceOfTruck(double daysToAddForStartingDate, int startMileageData, int endMileageData, decimal expectedTotalPrice)
        {
            VehicleCategories type = VehicleCategories.TRUCK;
            DateTime endDate = DateTime.Now;
            DateTime startDate = DateTime.Now.AddDays(daysToAddForStartingDate);
            int startMileage = startMileageData;
            int endMileage = endMileageData;

            BookingPriceCalculator carRentalPriceCalculator = new(type, startDate, endDate, startMileage, endMileage);
            decimal totalPrice = carRentalPriceCalculator.CalculateTotalPriceOfBooking();
            decimal expected = expectedTotalPrice;
            Assert.Equal(expected, totalPrice);
        }
    }
}
