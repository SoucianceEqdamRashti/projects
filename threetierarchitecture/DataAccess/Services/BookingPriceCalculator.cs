using BookingApi.DataAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingApi.DataAccess.Models.CategoryModel;

namespace BookingApi.DataAccess.Services
{
    public class BookingPriceCalculator
    {
        private VehicleCategories Category { get; }
        private DateTime StartDate { get; }
        private DateTime EndDate { get; }
        private int StartMileage { get; }
        private int EndMileage { get; }

        public const decimal BaseDailyRate = 500.00M;

        public const decimal BaseKMPrice = 2.10M;

        public const decimal FixedPriceTruckRate = 1.5M;

        public const decimal FixedPriceKombiRate = 1.3M;
        public BookingPriceCalculator(VehicleCategories category, DateTime startDate, DateTime endDate, int startMileage, int endMileage)
        {
            Category = category;
            StartDate = startDate;
            EndDate = endDate;
            StartMileage = startMileage;
            EndMileage = endMileage;
        }
        
        public decimal CalculateTotalPriceOfBooking()
        {            
            if (Category == VehicleCategories.SMALLCAR) {
                return CalculateTotalPriceSmallCar();
            }
            else if (Category == VehicleCategories.MEDIUMCAR) {
                return CalculateTotalPriceKombiCar();
            }
            else if (Category == VehicleCategories.TRUCK) {
                return CalculateTotalPriceTruck();
            }
            else
            {
                throw new UnknownVehicleCategoryException($"Category {Category} not found");
            }
        }

        private decimal CalculateTotalPriceSmallCar()
        {
            decimal totalPrice = Math.Round(CalculateNumberOfFractionalDays() * BaseDailyRate, 2);
            return totalPrice;
        }

        private decimal CalculateTotalPriceKombiCar()
        {
            decimal totalPrice = (BaseDailyRate * CalculateNumberOfFractionalDays() * FixedPriceKombiRate) 
                + (BaseKMPrice * CalculateMileageDifference());
            return totalPrice;
        }

        private decimal CalculateTotalPriceTruck()
        {
            decimal totalPrice = (BaseDailyRate * CalculateNumberOfFractionalDays() * FixedPriceTruckRate) 
                + (BaseKMPrice * CalculateMileageDifference() * FixedPriceTruckRate);
            return totalPrice;
        }

        private decimal CalculateNumberOfFractionalDays()
        {
            decimal totalFractionalDays = Math.Round(Convert.ToDecimal((EndDate - StartDate).TotalDays), 2);
            return totalFractionalDays;
            
        }

        private int CalculateMileageDifference()
        {
            return EndMileage - StartMileage;
        }
    }
}
