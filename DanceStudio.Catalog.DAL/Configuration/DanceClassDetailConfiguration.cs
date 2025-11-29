using DanceStudio.Catalog.Domain;
using DanceStudio.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DanceStudio.Catalog.DAL.Configuration
{
    public class DanceClassDetailConfiguration : IEntityTypeConfiguration<DanceClassDetail>
    {
        public void Configure(EntityTypeBuilder<DanceClassDetail> builder)
        {
            builder.ToTable("dance_class_details");

            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedNever();

            builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
            builder.Property(d => d.VideoUrl).HasColumnName("videourl").HasMaxLength(500).IsRequired(false);
            builder.Property(d => d.Requirements).HasColumnName("requirements").HasMaxLength(1000).IsRequired(false);


            builder.HasOne(d => d.DanceClass)
                   .WithOne(c => c.Details)
                   .HasForeignKey<DanceClassDetail>(d => d.Id);
        }
    }
}