using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using Npgsql;
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
            var sql = "SELECT BookingId, PaymentIntentId, Status, Amount, PaidAt FROM bookingpayments WHERE bookingid = @bookingId";
            await using var command = new NpgsqlCommand(sql, _connection, _transaction);

            command.Parameters.AddWithValue("bookingId", bookingId);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {

                return new BookingPayment
                {
                    BookingId = reader.GetInt64(reader.GetOrdinal("BookingId")),
                    PaymentIntentId = reader.GetString(reader.GetOrdinal("PaymentIntentId")),
                    Status = reader.GetInt16(reader.GetOrdinal("Status")),
                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),

                    PaidAt = reader.IsDBNull(reader.GetOrdinal("PaidAt")) ? null : reader.GetDateTime(reader.GetOrdinal("PaidAt"))
                };
            }

            return null;
        }
    }
}