using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Domain.Entities
{
    public class DanceClass
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty;
        public decimal DefaultPrice { get; set; }

      
        public DanceClassDetail? Details { get; set; }

    
        public ICollection<ClassInstructor> ClassInstructors { get; set; } = new List<ClassInstructor>();
    }
}
