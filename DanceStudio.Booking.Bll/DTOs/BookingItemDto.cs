using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Bll.DTOs
{
    public class BookingItemDto
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public long ClassScheduleId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
