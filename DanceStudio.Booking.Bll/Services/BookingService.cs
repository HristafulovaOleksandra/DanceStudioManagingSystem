using AutoMapper;
using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Exceptions;
using DanceStudio.Booking.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        
        public BookingService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BookingDetailsDto> CreateNewBookingAsync(CreateBookingDto dto)
        {
            
            try
            {
                await _uow.BeginTransactionAsync();
                long newBookingId = await _uow.Bookings.CreateNewBookingAsync(
                    dto.ClientId, dto.ClassScheduleId, dto.ClassName, dto.Price
                );
                await _uow.CommitAsync();

                return await GetBookingDetailsAsync(newBookingId);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw new ApplicationException("Error creating booking.", ex);
            }
        }

        public async Task<BookingDetailsDto> GetBookingDetailsAsync(long id)
        {
            var booking = await _uow.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new NotFoundException("Booking", id);

            var items = await _uow.BookingItems.GetItemsForBookingAsync(id);

            var bookingDto = _mapper.Map<BookingDetailsDto>(booking);
            bookingDto.Items = _mapper.Map<List<BookingItemDto>>(items);

            return bookingDto;
        }

        public async Task<IEnumerable<BookingSummaryDto>> GetBookingsForClientAsync(long clientId)
        {
            var summaries = await _uow.Bookings.GetBookingsForClientAsync(clientId);
            return _mapper.Map<IEnumerable<BookingSummaryDto>>(summaries);
        }

        public async Task ConfirmPaymentAsync(long bookingId, ConfirmPaymentDto dto)
        {
            await GetBookingDetailsAsync(bookingId);

            try
            {
                await _uow.BeginTransactionAsync();
                await _uow.Bookings.ConfirmBookingPaymentAsync(bookingId, dto.PaymentIntentId, dto.Amount);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw new ApplicationException("Error confirming payment.", ex);
            }
        }

        public async Task CancelBookingAsync(long bookingId)
        {
            await GetBookingDetailsAsync(bookingId);

            try
            {
                await _uow.BeginTransactionAsync();
                await _uow.Bookings.CancelBookingAsync(bookingId);
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                throw new ApplicationException("Error cancelling booking.", ex);
            }
        }
    }
}
