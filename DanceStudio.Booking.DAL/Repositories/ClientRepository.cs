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
            await using var cmd = new NpgsqlCommand("SELECT * FROM Clients WHERE Id = @id AND IsDeleted = FALSE", _connection);

            // Прив'язуємо команду до транзакції (важливо для UoW)
            cmd.Transaction = _transaction;

            // Використовуємо параметри для захисту від SQL-ін'єкцій
            cmd.Parameters.AddWithValue("id", id);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                
                return new Client
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
                };
            }
            return null;
        }

        //Dapper
        public async Task<Client?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Clients WHERE Email = @Email AND IsDeleted = FALSE";
            return await _connection.QueryFirstOrDefaultAsync<Client>(
                sql,
                new { Email = email },
                transaction: _transaction
            );
        }

        public async Task<long> CreateAsync(Client client)
        {
            var sql = @"
                INSERT INTO Clients (FirstName, LastName, Email, Phone)
                VALUES (@FirstName, @LastName, @Email, @Phone)
                RETURNING Id";

            return await _connection.QuerySingleAsync<long>(
                sql,
                client,
                transaction: _transaction
            );
        }
    }
}
