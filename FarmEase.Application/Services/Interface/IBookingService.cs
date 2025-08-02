using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;

namespace FarmEase.Application.Services.Interface
{
    public interface IBookingService
    {
        Task<string> CreateBooking(BookingModel bookingModel);
        Task<Booking> GetBookingById(int bookingId);
        Task<IEnumerable<Booking>> GetAllBookings(string userId);
        Task<BookingDetails> GetBookingDetails(int farmId, DateTime checkInDate, int nights);
    }
}
