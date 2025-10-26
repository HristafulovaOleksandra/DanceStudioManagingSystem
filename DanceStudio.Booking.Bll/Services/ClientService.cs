using AutoMapper;
using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Exceptions;
using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ClientDto> GetByIdAsync(long id)
        {
            var client = await _uow.Clients.GetByIdAsync(id);
            if (client == null)
                throw new NotFoundException(nameof(Client), id);

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            var existingClient = await _uow.Clients.GetByEmailAsync(dto.Email);
            if (existingClient != null)
                throw new ApplicationException("Client with this email already exists.");

            var newClient = _mapper.Map<Client>(dto);

            var newId = await _uow.Clients.CreateAsync(newClient);
            newClient.Id = newId;

            return _mapper.Map<ClientDto>(newClient);
        }

        public async Task<ClientDto> UpdateAsync(UpdateClientDto dto)
        {
            var clientToUpdate = await _uow.Clients.GetByIdAsync(dto.Id);
            if (clientToUpdate == null)
                throw new NotFoundException(nameof(Client), dto.Id);

            _mapper.Map(dto, clientToUpdate);


            return _mapper.Map<ClientDto>(clientToUpdate);
        }
        public async Task DeleteAsync(long id)
        {
            var clientToDelete = await _uow.Clients.GetByIdAsync(id);
            if (clientToDelete == null)
                throw new NotFoundException(nameof(Client), id);

            try
            {
                await _uow.BeginTransactionAsync();

                await _uow.Clients.DeleteAsync(id);

                await _uow.CommitAsync(); 
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw new ApplicationException("Error deleting client.", ex);
            }
        }
    }
}
