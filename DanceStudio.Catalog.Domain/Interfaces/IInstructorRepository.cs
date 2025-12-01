using DanceStudio.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Domain.Interfaces
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task AddAsync(Instructor instructor);
        Task<IEnumerable<Instructor>> GetInstructorsWithClassesAsync();
    }
}
