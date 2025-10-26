using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudioManagingSystem.Controllers
{
    [ApiController]
    [Route("api/bookings/{bookingId}/payment")]
    public class BookingPaymentsController : ControllerBase
    {
        private readonly IBookingPaymentService _paymentService;

        public BookingPaymentsController(IBookingPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET /api/bookings/{bookingId}/payment
        [HttpGet]
        [ProducesResponseType(typeof(BookingPaymentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentForBooking(long bookingId)
        {
            var payment = await _paymentService.GetByBookingIdAsync(bookingId);
            return Ok(payment);
        }
    }
}
