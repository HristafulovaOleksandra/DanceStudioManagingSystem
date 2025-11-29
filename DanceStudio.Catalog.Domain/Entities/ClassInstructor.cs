using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Domain.Entities
{
    public class ClassInstructor
    {
        public long DanceClassId { get; set; }
        public DanceClass DanceClass { get; set; } = default!;

        public long InstructorId { get; set; }
        public Instructor Instructor { get; set; } = default!;
    }
}
