using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IBookingRepository Bookings { get; }
        IBookingItemRepository BookingItems { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
