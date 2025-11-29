using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DanceStudio.Catalog.DAL.Configuration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("instructors");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(i => i.FirstName).HasColumnName("firstname").HasMaxLength(100).IsRequired();
            builder.Property(i => i.LastName).HasColumnName("lastname").HasMaxLength(100).IsRequired();
            builder.Property(i => i.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            builder.Property(i => i.Bio).HasColumnName("bio").HasMaxLength(1000).IsRequired(false);
            builder.Property(i => i.IsActive).HasColumnName("isactive").IsRequired();
            builder.Property(i => i.CreatedAt).HasColumnName("createdat").IsRequired().HasDefaultValueSql("NOW()");

            builder.HasIndex(i => i.Email).IsUnique();

            builder.HasMany(i => i.ClassInstructors)
                   .WithOne(ci => ci.Instructor)
                   .HasForeignKey(ci => ci.InstructorId);
        }
    }
}
