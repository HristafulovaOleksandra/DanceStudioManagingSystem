using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BookingEntity = DanceStudio.Booking.Domain.Entities.Booking;
namespace DanceStudio.Booking.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction? _transaction;

        public BookingRepository(NpgsqlConnection connection, NpgsqlTransaction? transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        // fn_create_booking
        public async Task<long> CreateNewBookingAsync(long clientId, long classScheduleId, string className, decimal price)
        {
            var sql = "SELECT fn_create_booking(@p_client_id, @p_class_schedule_id, @p_class_name, @p_price)";

            return await _connection.QuerySingleAsync<long>(sql,
                new
                {
                    p_client_id = clientId,
                    p_class_schedule_id = classScheduleId,
                    p_class_name = className,
                    p_price = price
                },
                transaction: _transaction);
        }

        //fn_confirm_booking_payment
        public async Task ConfirmBookingPaymentAsync(long bookingId, string paymentIntentId, decimal amountPaid)
        {
            var sql = "SELECT fn_confirm_booking_payment(@p_booking_id, @p_payment_intent_id, @p_amount_paid)";

            await _connection.ExecuteAsync(sql,
                new
                {
                    p_booking_id = bookingId,
                    p_payment_intent_id = paymentIntentId,
                    p_amount_paid = amountPaid
                },
                transaction: _transaction);
        }

        //fn_cancel_booking
        public async Task CancelBookingAsync(long bookingId)
        {
            var sql = "SELECT fn_cancel_booking(@p_booking_id)";
            await _connection.ExecuteAsync(sql, new { p_booking_id = bookingId }, transaction: _transaction);
        }

        //fn_get_client_bookings
        public async Task<IEnumerable<ClientBookingSummary>> GetBookingsForClientAsync(long clientId)
        {
            var sql = "SELECT * FROM fn_get_client_bookings(@p_client_id)";

            return await _connection.QueryAsync<ClientBookingSummary>(sql,
                new { p_client_id = clientId },
                transaction: _transaction);
        }

        // 5. Простий Dapper-запит (використовуємо псевдонім BookingEntity)
        public async Task<BookingEntity?> GetByIdAsync(long id)
        {
            var sql = "SELECT * FROM Bookings WHERE Id = @Id AND IsDeleted = FALSE";
            return await _connection.QueryFirstOrDefaultAsync<BookingEntity>(
                sql,
                new { Id = id },
                transaction: _transaction
            );
        }

    }
}
