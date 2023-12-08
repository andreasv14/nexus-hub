using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusHub.Infrastructure.Persistence.Entities;

namespace NexusHub.Infrastructure.Persistence.Configurations;

public class CompanySiteEntityConfiguration : IEntityTypeConfiguration<CompanySiteEntity>
{
    public void Configure(EntityTypeBuilder<CompanySiteEntity> builder)
    {
        builder.ToTable("CompanySites");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(250);

        builder.Property(x => x.IsMainBranch)
            .IsRequired();

        //builder.HasOne(x => x.Company)
        //    .WithMany(x => x.CompanySites)
        //    .HasForeignKey(x => x.CompanyId);
    }
}