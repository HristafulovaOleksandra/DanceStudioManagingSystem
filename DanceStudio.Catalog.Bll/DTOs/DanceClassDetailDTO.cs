using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.DTOs
{
    public class DanceClassDetailDTO
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }= string.Empty;
        public string? Requirements { get; set; }
        public long DanceClassId { get; set; }
    }

    public class DanceClassDetailCreateUpdateDTO
    {
        public string Description { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;

        public string? VideoUrl { get; set; } = string.Empty;
        public string? Requirements { get; set; } = string.Empty;
        public long DanceClassId { get; set; }
    }
}
