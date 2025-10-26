using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DanceStudio.Booking.Bll.DTOs
{
   
    public class BookingDetailsDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<BookingItemDto> Items { get; set; } = new();
    }

    
    public class BookingSummaryDto
    {
        public long BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public long ItemCount { get; set; }
    }

    
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "Client ID is required")]
        public long ClientId { get; set; }

        [Required(ErrorMessage = "Class Schedule ID is required")]
        public long ClassScheduleId { get; set; }

        [Required(ErrorMessage = "Class Name is required")]
        [StringLength(200, MinimumLength = 3)]
        public string ClassName { get; set; } = string.Empty;

        [Range(1, 10000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }

    
    public class ConfirmPaymentDto
    {
        [Required]
        public string PaymentIntentId { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.01")]
        public decimal Amount { get; set; }
    }
}