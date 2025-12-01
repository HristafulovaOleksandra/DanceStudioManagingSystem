using DanceStudio.Catalog.Domain.Entities;

namespace DanceStudio.Catalog.Domain.Specifications.DanceClassSpecs
{
    public class DanceClassesSpecification : BaseSpecification<DanceClass>
    {
        public DanceClassesSpecification(string? difficultyLevel, string? sort, int pageIndex, int pageSize, string? search)
            : base(x =>
                (string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search.ToLower())) &&
                (string.IsNullOrEmpty(difficultyLevel) || x.DifficultyLevel == difficultyLevel)
            )
        {
            AddOrderBy(n => n.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.DefaultPrice);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.DefaultPrice);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

            ApplyPaging(pageSize * (pageIndex - 1), pageSize);
        }
    }
}