using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanceStudio.Booking.Domain.Entities;
using BookingEntity = DanceStudio.Booking.Domain.Entities.Booking;

namespace DanceStudio.Booking.DAL.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<BookingEntity?> GetByIdAsync(long id);
        Task<IEnumerable<ClientBookingSummary>> GetBookingsForClientAsync(long clientId);

        // call from PostgreSQL
        Task<long> CreateNewBookingAsync(long clientId, long classScheduleId, string className, decimal price);
        Task ConfirmBookingPaymentAsync(long bookingId, string paymentIntentId, decimal amountPaid); //!!
        Task CancelBookingAsync(long bookingId);
    }
}
