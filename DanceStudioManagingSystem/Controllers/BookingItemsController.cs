using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Bll.Services;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudioManagingSystem.Controllers
{
    [ApiController]
    [Route("api")]
    public class BookingItemsController : ControllerBase
    {
        private readonly IBookingItemService _itemService;

        public BookingItemsController(IBookingItemService itemService)
        {
            _itemService = itemService;
        }

        // GET /api/bookings/{bookingId}/items
        [HttpGet("bookings/{bookingId}/items")]
        [ProducesResponseType(typeof(IEnumerable<BookingItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemsForBooking(long bookingId)
        {
            var items = await _itemService.GetItemsForBookingAsync(bookingId);
            return Ok(items);
        }

        // GET /api/bookingitems/{id}
        [HttpGet("bookingitems/{id}")]
        [ProducesResponseType(typeof(BookingItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingItemById(long id)
        {
            var item = await _itemService.GetByIdAsync(id);
            return Ok(item);
        }
    }
}
