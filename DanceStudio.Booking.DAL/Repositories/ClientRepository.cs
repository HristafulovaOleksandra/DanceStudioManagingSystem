using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using Dapper;
using Npgsql;

namespace DanceStudio.Booking.Dal
{
    public class ClientRepository : IClientRepository
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction? _transaction;

        public ClientRepository(NpgsqlConnection connection, NpgsqlTransaction? transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

 
        public async Task<Client?> GetByIdAsync(long id)
        {
            var sql = "SELECT * FROM clients WHERE id = @id AND isdeleted = FALSE";
            return await _connection.QueryFirstOrDefaultAsync<Client>(
                sql,
                new { id },
                transaction: _transaction
            );
        }


        public async Task<Client?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM clients WHERE email = @Email AND isdeleted = FALSE";
            return await _connection.QueryFirstOrDefaultAsync<Client>(
                sql,
                new { Email = email },
                transaction: _transaction
            );
        }


        public async Task<long> CreateAsync(Client client)
        {
            var sql = @"
                INSERT INTO clients (firstname, lastname, email, phone)
                VALUES (@FirstName, @LastName, @Email, @Phone)
                RETURNING id";

            return await _connection.QuerySingleAsync<long>(
                sql,
                client,
                transaction: _transaction
            );
        }

        public async Task UpdateAsync(Client entity)
        {
            var sql = @"
                UPDATE clients SET
                    firstname = @FirstName,
                    lastname = @LastName,
                    email = @Email,
                    phone = @Phone,
                WHERE id = @Id;";

            await _connection.ExecuteAsync(sql, entity, transaction: _transaction);
        }


        public async Task DeleteAsync(long id)
        {
            var sql = @"
                UPDATE clients 
                SET isdeleted = TRUE
                WHERE id = @id AND isdeleted = FALSE";

            await _connection.ExecuteAsync(sql, new { id }, transaction: _transaction);
        }
    }
}