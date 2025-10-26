using DanceStudio.Booking.DAL.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using DanceStudio.Booking.Dal;
namespace DanceStudio.Booking.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;
        private bool _disposed; 

        private IClientRepository? _clientRepository;
        private IBookingRepository? _bookingRepository;
        private IBookingItemRepository? _bookingItemRepository;


        public UnitOfWork(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public IClientRepository Clients =>
            _clientRepository ??= new ClientRepository(_connection, _transaction);

        public IBookingRepository Bookings =>
            _bookingRepository ??= new BookingRepository(_connection, _transaction);

        public IBookingItemRepository BookingItems =>
            _bookingItemRepository ??= new BookingItemRepository(_connection, _transaction);


        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction is already started.");

            _transaction = await _connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
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
            }
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
