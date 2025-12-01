using DanceStudio.Catalog.Domain.Entities;

namespace DanceStudio.Catalog.Domain.Specifications.DanceClassSpecs
{
    public class DanceClassesWithFiltersForCountSpecification : BaseSpecification<DanceClass>
    {
        public DanceClassesWithFiltersForCountSpecification(string? difficultyLevel, string? search)
            : base(x =>
                (string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search.ToLower())) &&
                (string.IsNullOrEmpty(difficultyLevel) || x.DifficultyLevel == difficultyLevel)
            )
        {
        }
    }
}