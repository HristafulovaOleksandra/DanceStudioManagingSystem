using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.DAL.Repositories
{
    public class DanceClassDetailRepository : GenericRepository<DanceClassDetail>, IDanceClassDetailRepository
    {
        public DanceClassDetailRepository(CatalogDbContext context) : base(context)
        {
        }

        //Explicit Loading
        public async Task<DanceClassDetail?> GetDetailWithClassExplicitlyAsync(long detailId)
        {
            var detail = await _dbSet.FindAsync(detailId);

            if (detail != null)
            {
                await _context.Entry(detail)
                    .Reference(d => d.DanceClass) 
                    .LoadAsync();
            }

            return detail;
        }
    }
}