using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.DTOs
{
    public class BookingPaymentDto
    {
        public long BookingId { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public short Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
