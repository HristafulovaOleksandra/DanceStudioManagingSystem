using DanceStudio.Booking.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.Services
{
    public interface IClientService
    {
        Task<ClientDto> GetByIdAsync(long id);
        Task<ClientDto> CreateAsync(CreateClientDto dto);
        Task<ClientDto> UpdateAsync(UpdateClientDto dto);
        
    }
}
