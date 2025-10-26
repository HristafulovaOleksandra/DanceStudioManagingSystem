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
            await using var cmd = new NpgsqlCommand("SELECT * FROM Clients WHERE Id = @id AND IsDeleted = FALSE", _connection);
            cmd.Transaction = _transaction;
            cmd.Parameters.AddWithValue("id", id);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Client
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Email = reader.GetString(reader.GetOrdinal("email")),
                    Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat")),
                    IsDeleted = reader.GetBoolean(reader.GetOrdinal("isdeleted"))
                };
            }
            return null;
        }

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