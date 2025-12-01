using DanceStudio.Catalog.Domain.Entities;
namespace DanceStudio.Catalog.Domain.Interfaces
{
    public interface IDanceClassDetailRepository : IGenericRepository<DanceClassDetail>
    {
        Task<DanceClassDetail?> GetDetailWithClassExplicitlyAsync(long detailId);
    }
}