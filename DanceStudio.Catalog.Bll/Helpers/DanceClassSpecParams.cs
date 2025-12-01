namespace DanceStudio.Catalog.Bll.Helpers
{
    public class DanceClassSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? DifficultyLevel { get; set; }
        public string? Sort { get; set; } 
        public string? Search { get; set; }
    }
}