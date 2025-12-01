using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Domain.Entities
{
    public class DanceClassDetail
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public string? Requirements { get; set; }
        public DanceClass DanceClass { get; set; } = default!;
    }
}
