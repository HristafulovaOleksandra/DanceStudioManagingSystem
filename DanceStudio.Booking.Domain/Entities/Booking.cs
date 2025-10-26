using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Domain.Entities
{
    public class Booking
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public short Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } // DateTime? (nullable)
        public bool IsDeleted { get; set; }
    }
}
