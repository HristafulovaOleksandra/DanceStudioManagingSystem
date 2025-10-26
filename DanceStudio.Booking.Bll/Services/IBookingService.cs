using DanceStudio.Booking.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.Services
{
    public interface IBookingService
    {
        Task<BookingDetailsDto> CreateNewBookingAsync(CreateBookingDto dto);
        Task<BookingDetailsDto> GetBookingDetailsAsync(long id);
        Task<IEnumerable<BookingSummaryDto>> GetBookingsForClientAsync(long clientId);
        Task ConfirmPaymentAsync(long bookingId, ConfirmPaymentDto dto);
        Task CancelBookingAsync(long bookingId);
    }
}
