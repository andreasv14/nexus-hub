using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusHub.Infrastructure.Identity.Models;
using NexusHub.Infrastructure.Persistence.Entities;

namespace NexusHub.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, Guid>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CompanyEntity> Companies { get; set; }

    public DbSet<CompanySiteEntity> CompanySites { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: Add audit interceptor
        base.OnConfiguring(optionsBuilder);
    }
}