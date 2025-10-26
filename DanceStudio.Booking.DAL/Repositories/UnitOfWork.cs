using DanceStudio.Booking.DAL.Repositories.Interfaces;
using Npgsql;
using System;
using System.Data;
using DanceStudio.Booking.Dal;

namespace DanceStudio.Booking.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;
        private bool _disposed;

  
        public IClientRepository Clients { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IBookingItemRepository BookingItems { get; private set; }

        public IBookingPaymentRepository BookingPayments { get; private set; }

        public UnitOfWork(NpgsqlConnection connection)
        {
            _connection = connection;

            Clients = new ClientRepository(_connection, null);
            Bookings = new BookingRepository(_connection, null);
            BookingItems = new BookingItemRepository(_connection, null);
            BookingPayments = new BookingPaymentRepository(_connection, null);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction is already started.");

            _transaction = await _connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            Clients = new ClientRepository(_connection, _transaction);
            Bookings = new BookingRepository(_connection, _transaction);
            BookingItems = new BookingItemRepository(_connection, _transaction);
            BookingPayments = new BookingPaymentRepository(_connection, _transaction); // <-- 4. ДОДАЙ ЦЕ
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction is not started.");

            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
                ResetRepositories();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction is not started.");

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            Clients = new ClientRepository(_connection, null);
            Bookings = new BookingRepository(_connection, null);
            BookingItems = new BookingItemRepository(_connection, null);
            BookingPayments = new BookingPaymentRepository(_connection, null);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }

            _disposed = true;
        }
    }
}