using DanceStudio.Catalog.DAL.Configuration;
using DanceStudio.Catalog.Domain;
using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Catalog.DAL
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Instructor> Instructors { get; set; } = default!;

        public DbSet<DanceClass> DanceClasses { get; set; } = default!;
        public DbSet<DanceClassDetail> DanceClassDetails { get; set; } = default!;
        public DbSet<ClassInstructor> ClassInstructors { get; set; } = default!;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}