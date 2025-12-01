using DanceStudio.Catalog.Domain.Entities;

namespace DanceStudio.Catalog.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInstructorRepository Instructors { get; }
        IDanceClassRepository DanceClasses { get; }
        IDanceClassDetailRepository ClassDetails { get; }

        Task<int> CompleteAsync();
    }
}