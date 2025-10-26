using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace DanceStudio.Booking.DAL.Repositories
{
    public class BookingItemRepository : IBookingItemRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction? _transaction;

        public BookingItemRepository(NpgsqlConnection connection, NpgsqlTransaction? transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<BookingItem>> GetItemsForBookingAsync(long bookingId)
        {
            var sql = "SELECT * FROM BookingItems WHERE BookingId = @BookingId";
            return await _connection.QueryAsync<BookingItem>(sql,
                new { BookingId = bookingId },
                transaction: _transaction
            );
        }
    }
}
