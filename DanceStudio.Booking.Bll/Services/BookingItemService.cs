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
    public class BookingItemService : IBookingItemService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookingItemService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BookingItemDto> GetByIdAsync(long id)
        {
            var item = await _uow.BookingItems.GetByIdAsync(id);
            if (item == null)
                throw new NotFoundException(nameof(BookingItem), id);

            return _mapper.Map<BookingItemDto>(item);
        }

        public async Task<IEnumerable<BookingItemDto>> GetItemsForBookingAsync(long bookingId)
        {
            var parentBooking = await _uow.Bookings.GetByIdAsync(bookingId);
            if (parentBooking == null)
                throw new NotFoundException(nameof(Domain.Entities.Booking), bookingId);

            var items = await _uow.BookingItems.GetItemsForBookingAsync(bookingId);
            return _mapper.Map<IEnumerable<BookingItemDto>>(items);
        }
    }
}
