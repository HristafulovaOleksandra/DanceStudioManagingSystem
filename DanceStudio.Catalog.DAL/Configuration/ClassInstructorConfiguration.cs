using DanceStudio.Catalog.Domain;
using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Catalog.DAL.Configuration
{
    public class ClassInstructorConfiguration : IEntityTypeConfiguration<ClassInstructor>
    {
        public void Configure(EntityTypeBuilder<ClassInstructor> builder)
        {
            builder.ToTable("class_instructors");

            builder.HasKey(ci => new { ci.DanceClassId, ci.InstructorId });

            builder.Property(ci => ci.DanceClassId).HasColumnName("danceclassid").IsRequired();
            builder.Property(ci => ci.InstructorId).HasColumnName("instructorid").IsRequired();

            builder.HasOne(ci => ci.DanceClass)
                   .WithMany(c => c.ClassInstructors)
                   .HasForeignKey(ci => ci.DanceClassId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(ci => ci.Instructor)
                   .WithMany(i => i.ClassInstructors)
                   .HasForeignKey(ci => ci.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}