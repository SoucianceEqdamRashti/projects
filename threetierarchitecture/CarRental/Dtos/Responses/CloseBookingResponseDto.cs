namespace BookingApi.Dtos.Responses
{
    public class CloseBookingResponseDto
    {
        public string BookingNumber { get; set; }   

        //Note this returns 1000 or 1000.5 or 1000.51
        //To force the response to always have two decimal points to need to implement custom json converter
        public decimal TotalPrice { get; set; }
    }
}
