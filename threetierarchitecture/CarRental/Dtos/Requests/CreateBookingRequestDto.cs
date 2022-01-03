namespace BookingApi.Dtos.Requests;
public class CreateBookingRequestDto
{
    public string PersonNumber { get; set; }
    public DateTime DateOfBooking { get; set; }
}
