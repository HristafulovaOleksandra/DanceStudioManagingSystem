using DanceStudio.Catalog.Domain;
using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Catalog.DAL.Configuration
{
    public class DanceClassConfiguration : IEntityTypeConfiguration<DanceClass>
    {
        public void Configure(EntityTypeBuilder<DanceClass> builder)
        {
            builder.ToTable("dance_classes");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            builder.Property(c => c.DifficultyLevel).HasColumnName("difficultylevel").HasMaxLength(50).IsRequired();

            builder.Property(c => c.DefaultPrice).HasColumnName("defaultprice").HasColumnType("decimal(18, 2)").IsRequired();

            builder.HasOne(c => c.Details)
                   .WithOne(d => d.DanceClass)
                   .HasForeignKey<DanceClassDetail>(d => d.Id) 
                   .IsRequired(false); 

            builder.HasMany(c => c.ClassInstructors)
                   .WithOne(ci => ci.DanceClass)
                   .HasForeignKey(ci => ci.DanceClassId);
        }
    }
}