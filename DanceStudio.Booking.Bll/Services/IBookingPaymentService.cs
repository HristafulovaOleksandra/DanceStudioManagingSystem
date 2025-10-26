using DanceStudio.Booking.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.Services
{
    public interface IBookingPaymentService
    {
        Task<BookingPaymentDto> GetByBookingIdAsync(long bookingId);
    }
}
