using Dapper;
using BookingApi.DataAccess.Exceptions;
using BookingApi.DataAccess.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Repository
{
    public class BookingRepository : IDataAccessRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<BookingRepository> _logger;
        public BookingRepository(IDbConnectionFactory dbConnectionFactory, ILogger<BookingRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;   
        }
        public async Task<int> AddCustomerAsync(CustomerModel customer)
        {
            var personNumber = customer.PersonNumber;
            var parameters = new { PersonNumber = personNumber, CustomerCreationDate = customer.CustomerCreationDate };            
            _logger.LogInformation("Adding customer with person number {personNumber} ", personNumber);
            const string query = @"INSERT INTO [dbo].[Customers] ([PersonNumber], [CustomerCreationDate]) OUTPUT INSERTED.CustomerId Values(@PersonNumber, @CustomerCreationDate);";            
            using var connection = _dbConnectionFactory.CreateConnection();
            var customerId = await connection.QuerySingleAsync<int>(query, parameters);
            return customerId;
        }

        public async Task AddBookingAsync(RentalModel booking)
        {
            var bookingNumber = booking.BookingNumber;
            var parameters = new {
                CustomerId = booking.CustomerId,
                VehicleId = booking.VehicleId,                
                StartMileage = booking.StartMileage,
                StartDate = booking.StartDate,
                BookingNumber = bookingNumber
            };
            _logger.LogInformation("Adding booking with booking number {bookingNumber} ", bookingNumber);
            string query = @"INSERT INTO [dbo].[Bookings] ([CustomerId],[VehicleId],[StartMileage],[StartDate],[BookingNumber])";
            query = query + " " + "VALUES (@CustomerId, @VehicleId, @StartMileage,@StartDate, @BookingNumber);";
            using var connection = _dbConnectionFactory.CreateConnection();
            try
            {
                await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception ex)
            {
                throw new AddBookingToDatabaseException("Unable to add booking to database", ex);
            }            
        
        }

        public async Task<IEnumerable<VehicleModel>> GetVehicleByRegistrationNumberAsync(string registrationNumber)
        {
            var parameters = new { RegistrationNumber = registrationNumber };
            _logger.LogInformation("Finding if car with registration number {registrationNumber} exists ", registrationNumber);
            const string query = @"SELECT * FROM dbo.Vehicles WHERE RegistrationNumber = @RegistrationNumber;";
            using var connection = _dbConnectionFactory.CreateConnection();
            var vehicleFound = await connection.QueryAsync<VehicleModel>(query, param:parameters);
            if (vehicleFound == null)
            {
                return Enumerable.Empty<VehicleModel>();
            }
            return vehicleFound;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomerByPersonNumberAsync(string personNumber)
        {
            var parameters = new { PersonNumber = personNumber };
            _logger.LogInformation("Finding if customer with person number {personNumber} exists ", personNumber);
            const string query = @"SELECT * FROM dbo.Customers WHERE PersonNumber = @PersonNumber;";
            using var connection = _dbConnectionFactory.CreateConnection();
            var customerFound = await connection.QueryAsync<CustomerModel>(query, param: parameters);
            if (customerFound == null)
            {
                return Enumerable.Empty<CustomerModel>();
            }
            return customerFound;
        }

        public async Task<IEnumerable<RentalModel>> GetBookingByBookingNumberAsync(string bookingNumber)
        {
            var parameters = new { BookingNumber = bookingNumber };
            _logger.LogInformation("Finding if booking with booking number {registrationNumber} exists ", bookingNumber);
            const string query = @"SELECT * FROM [dbo].[Bookings] WHERE BookingNumber = @BookingNumber;";
            using var connection = _dbConnectionFactory.CreateConnection();
            var bookingFound = await connection.QueryAsync<RentalModel>(query, param: parameters);
            if (bookingFound == null)
            {
                return Enumerable.Empty<RentalModel>();
            }
            return bookingFound;
        }

        public async Task UpdateBookingAndVehicleAsync(RentalModel booking, VehicleModel vehicle)
        {
            var bookingNumber = booking.BookingNumber;
            var rentalParameters = new
            {
                EndMileage = booking.EndMileage,
                EndDate = DateTime.UtcNow.ToString(),
                TotalPrice = booking.TotalPrice,
                BookingNumber = booking.BookingNumber
            };
            _logger.LogInformation("Updating booking with booking number {bookingNumber} ", bookingNumber);
            string rentalQuery = @"UPDATE [dbo].[Bookings] SET EndMileage=@EndMileage, EndDate=@EndDate, TotalPrice=@TotalPrice WHERE BookingNumber=@BookingNumber";

            var vehicleId = vehicle.VehicleId;
            var vehicleParameters = new
            {
                VehicleId = vehicleId,
                Mileage = vehicle.Mileage
            };
            _logger.LogInformation("Updating vehicle with vehicle id {vehicleId} ", vehicleId);
            string vehicleQuery = @"UPDATE [dbo].[Vehicles] SET Mileage=@Mileage WHERE VehicleId=@VehicleId";

            using var connection = _dbConnectionFactory.CreateConnection();
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                //room for improvement to log number of rows affected
                await connection.ExecuteAsync(rentalQuery, rentalParameters, transaction);
                _logger.LogInformation("Booking for booking number  {bookingNumber} updated ", bookingNumber);
                await connection.ExecuteAsync(vehicleQuery, vehicleParameters, transaction);
                _logger.LogInformation("Vehicle {vehicleId} updated with mileage ", vehicleId);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    connection.Close();
                }
                catch (Exception exRollback)
                {
                    throw new TransactionRollBackFailedException($"Unable to rollback transaction for booking with booking {bookingNumber}", exRollback);
                }
                throw new TransactionFailedException($"Unable to update booking and vehicle for booking number {bookingNumber}", ex);
            }
        }

        public async Task<IEnumerable<CategoryModel.VehicleCategories>> GetVehicleCategoryByVehicleIdAsync(int vehicleId)
        {
            var parameters = new { VehicleId = vehicleId };
            _logger.LogInformation("Finding category for a particular vehicle with id {vehicleId} ", vehicleId);
            string query = @"SELECT CategoryType FROM [dbo].Categories WHERE";
            query = string.Concat(query, " ", "[dbo].Categories.CategoryId = (SELECT CategoryId FROM [dbo].Vehicles WHERE VehicleId = @VehicleId)");
            using var connection = _dbConnectionFactory.CreateConnection();
            var bookingFound = await connection.QueryAsync<string>(query, param: parameters);
            if (bookingFound?.Any() != true)
            {
                return Enumerable.Empty<CategoryModel.VehicleCategories>();
            }
            if (Enum.TryParse(bookingFound.First(), out CategoryModel.VehicleCategories category)) {
                IEnumerable<CategoryModel.VehicleCategories> categoryOfRentedCar = new List<CategoryModel.VehicleCategories>
                {
                    category
                };
                return categoryOfRentedCar;
            }
            else
                throw new ArgumentException($"{bookingFound.First()} is not a valid category");
        }
    }
}
