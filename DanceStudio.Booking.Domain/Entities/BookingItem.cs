using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Domain.Entities
{
    public class BookingItem
    {
        public long Id { get; set; }
        public long BookingId { get; set; }
        public long ClassScheduleId { get; set; }
        public string ClassName { get; set; }
        public decimal Price { get; set; }
    }
}
