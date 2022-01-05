namespace BookingApi.Bookings.Dtos.Requests
{
    public class CloseBookingRequestDto
    {
        public int EndMileage { get; set; }
        public DateTime EndDateBooking { get; set; }    
    }
}
