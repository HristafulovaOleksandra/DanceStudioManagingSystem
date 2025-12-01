using DanceStudio.Catalog.Domain.Interfaces;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;

        // Поля для Lazy Initialization
        private IInstructorRepository? _instructorRepository;
        private IDanceClassRepository? _danceClassRepository;
        private IDanceClassDetailRepository? _classDetailRepository;

        public UnitOfWork(CatalogDbContext context)
        {
            _context = context;
        }

        public IInstructorRepository Instructors =>
            _instructorRepository ??= new InstructorRepository(_context);

        public IDanceClassRepository DanceClasses =>
            _danceClassRepository ??= new DanceClassRepository(_context);

        public IDanceClassDetailRepository ClassDetails =>
            _classDetailRepository ??= new DanceClassDetailRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}