using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Domain.Entities
{
    public class BookingPayment
    {
        public long BookingId { get; set; } // PK та FK
        public required string PaymentIntentId { get; set; }
        public short Status { get; set; } // SMALLINT -> short
        public decimal Amount { get; set; }
        public DateTime? PaidAt { get; set; } // DateTime? (nullable)
    }
}
