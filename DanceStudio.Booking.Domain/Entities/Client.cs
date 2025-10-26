using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Booking.Domain.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; } // string? Phone (nullable)
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
