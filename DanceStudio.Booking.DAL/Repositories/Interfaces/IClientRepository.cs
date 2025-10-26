using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanceStudio.Booking.Domain.Entities;

namespace DanceStudio.Booking.DAL.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(long id);
        Task<Client?> GetByEmailAsync(string email);
        Task<long> CreateAsync(Client client);
    }
}
