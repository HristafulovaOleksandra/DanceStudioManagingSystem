using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Domain.Entities
{
    public class Instructor
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public ICollection<ClassInstructor> ClassInstructors { get; set; } = new List<ClassInstructor>();
    }
}
