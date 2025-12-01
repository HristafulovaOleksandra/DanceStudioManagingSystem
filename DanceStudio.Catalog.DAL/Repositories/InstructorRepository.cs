using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.DAL.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(CatalogDbContext context) : base(context)
        {
        }

        // Eager Loading
        // (багато-до-багатьох)
        public async Task<IEnumerable<Instructor>> GetInstructorsWithClassesAsync()
        {
            return await _dbSet
                .Include(i => i.ClassInstructors)
                    .ThenInclude(ci => ci.DanceClass)
                .ToListAsync();
        }
    }
}