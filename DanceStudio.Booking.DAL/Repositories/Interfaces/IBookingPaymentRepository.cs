using DanceStudio.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.DAL.Repositories.Interfaces
{
    public interface IBookingPaymentRepository
    {
        Task<BookingPayment?> GetByBookingIdAsync(long bookingId);

    }
}
