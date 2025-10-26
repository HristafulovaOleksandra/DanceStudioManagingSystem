using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Booking.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST /api/bookings
        [HttpPost]
        [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var createdBooking = await _bookingService.CreateNewBookingAsync(dto);
            return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.Id }, createdBooking);
        }

        // GET /api/bookings/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingById(long id)
        {
            var booking = await _bookingService.GetBookingDetailsAsync(id);
            return Ok(booking);
        }

        // GET /api/clients/{clientId}/bookings
        [HttpGet("/api/clients/{clientId}/bookings")]
        [ProducesResponseType(typeof(IEnumerable<BookingSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingsForClient(long clientId)
        {
            var bookings = await _bookingService.GetBookingsForClientAsync(clientId);
            return Ok(bookings);
        }

        // POST /api/bookings/{id}/confirm-payment
        [HttpPost("{id}/confirm-payment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmPayment(long id, [FromBody] ConfirmPaymentDto dto)
        {
            await _bookingService.ConfirmPaymentAsync(id, dto);
            return NoContent();
        }

        // DELETE /api/bookings/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelBooking(long id)
        {
            await _bookingService.CancelBookingAsync(id);
            return NoContent();
        }
    }
}