using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.DAL.Repositories
{
    public class BookingPaymentRepository : IBookingPaymentRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction? _transaction;

        public BookingPaymentRepository(NpgsqlConnection connection, NpgsqlTransaction? transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<BookingPayment?> GetByBookingIdAsync(long bookingId)
        {
            var sql = "SELECT * FROM bookingpayments WHERE bookingid = @bookingId";
            return await _connection.QueryFirstOrDefaultAsync<BookingPayment>(
                sql,
                new { bookingId },
                _transaction
            );
        }
    }
}
