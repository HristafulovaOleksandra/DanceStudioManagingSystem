using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.DTOs
{
    public class DanceClassDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty;
        public decimal DefaultPrice { get; set; }

    }

    public class DanceClassCreateUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty;
        public decimal DefaultPrice { get; set; }
    }
}
