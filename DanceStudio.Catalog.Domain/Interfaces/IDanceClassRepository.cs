using DanceStudio.Catalog.Domain.Entities;

namespace DanceStudio.Catalog.Domain.Interfaces
{
    public interface IDanceClassRepository : IGenericRepository<DanceClass>
    {
        Task<IEnumerable<DanceClass>> GetClassesByInstructorNameAsync(string instructorName);
    }
}