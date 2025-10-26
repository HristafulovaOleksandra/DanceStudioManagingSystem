using DanceStudio.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DanceStudio.Booking.DAL.Repositories.Interfaces
{
    public interface IBookingItemRepository
    {
        Task<IEnumerable<BookingItem>> GetItemsForBookingAsync(long bookingId);
    }
}
