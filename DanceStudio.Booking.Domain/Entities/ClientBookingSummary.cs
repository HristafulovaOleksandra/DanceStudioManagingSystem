using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Domain.Entities
{
    // result of fn_get_client_bookings
    public class ClientBookingSummary
    {
        public long BookingId { get; set; }
        public short Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public long ItemCount { get; set; }
    }
}
