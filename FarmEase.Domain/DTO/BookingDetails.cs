namespace FarmEase.Domain.DTO
{
    public class BookingDetails
    {
        public FarmModel farm { get; set; }
        public int nights { get; set; }
        public double TotalCost { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
