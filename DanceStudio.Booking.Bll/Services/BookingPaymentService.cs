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
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookingPaymentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BookingPaymentDto> GetByBookingIdAsync(long bookingId)
        {
            var payment = await _uow.BookingPayments.GetByBookingIdAsync(bookingId);
            if (payment == null)
                throw new NotFoundException($"Payment for booking {bookingId} not found");

            return _mapper.Map<BookingPaymentDto>(payment);
        }
    }
}