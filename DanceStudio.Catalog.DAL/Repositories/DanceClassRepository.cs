using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.DAL.Repositories
{
    public class DanceClassRepository : GenericRepository<DanceClass>, IDanceClassRepository
    {
        public DanceClassRepository(CatalogDbContext context) : base(context)
        {
        }
        // LINQ to Entities 
        //(Багато-до-багатьох із використанням проміжної сутності)
        public async Task<IEnumerable<DanceClass>> GetClassesByInstructorNameAsync(string instructorName)
        {
            // Використовуємо зв'язок: DanceClass -> ClassInstructors -> Instructor
            return await _dbSet
                .Where(dc => dc.ClassInstructors.Any(
                    ci => ci.Instructor.FirstName == instructorName ||
                          ci.Instructor.LastName == instructorName))
                .Include(dc => dc.ClassInstructors)
                    .ThenInclude(ci => ci.Instructor)
                .ToListAsync();
        }
    }
}