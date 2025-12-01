using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.DAL
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

            await context.Database.MigrateAsync();

            if (await context.Instructors.AnyAsync())
            {
                return; 
            }

            var instructors = new List<Instructor>
            {
                new Instructor { FirstName = "Oksana", LastName = "Hristafulova", Email = "oksana@dance.com", Bio = "Pro Latin Dancer", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Instructor { FirstName = "John", LastName = "Doe", Email = "john@dance.com", Bio = "Hip-Hop Guru", IsActive = true, CreatedAt = DateTime.UtcNow }
            };
            await context.Instructors.AddRangeAsync(instructors);
            await context.SaveChangesAsync();

            var classes = new List<DanceClass>
            {
                new DanceClass { Name = "Salsa Basics", DifficultyLevel = "Beginner", DefaultPrice = 15.00m },
                new DanceClass { Name = "Advanced Hip-Hop", DifficultyLevel = "Advanced", DefaultPrice = 20.00m }
            };
            await context.DanceClasses.AddRangeAsync(classes);
            await context.SaveChangesAsync();

            var salsaClass = await context.DanceClasses.FirstOrDefaultAsync(c => c.Name == "Salsa Basics");
            var oksanaInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.FirstName == "Oksana");

            if (salsaClass != null && oksanaInstructor != null)
            {
                var link = new ClassInstructor
                {
                    DanceClassId = salsaClass.Id,
                    InstructorId = oksanaInstructor.Id
                };

                await context.ClassInstructors.AddAsync(link);
                await context.SaveChangesAsync();
            }
        }
    }
}