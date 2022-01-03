using BookingApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.DataAccess.Services
{
    public interface IBookingService
    {
        //Sending the person number as string
        //If validation of person number was stronger it should be sent as a person number object
        Task<AddBookingResponseModel> CreateBookingAsync(AddBookingRequestModel addBookingRequest);
        Task<CloseBookingResponseModel> CloseBookingAsync(CloseBookingRequestModel closeBookingRequest);
    }
}