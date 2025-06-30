using ICDify.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICDify.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<DrugEntity> Drugs => Set<DrugEntity>();
    public DbSet<IndicationEntity> Indications => Set<IndicationEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DrugEntity>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.HasIndex(d => d.Name).IsUnique();
            entity.Property(d => d.Name).IsRequired();

            entity.HasMany(d => d.Indications)
                  .WithOne(i => i.Drug)
                  .HasForeignKey(i => i.DrugId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IndicationEntity>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Condition).IsRequired();
            entity.Property(i => i.ICD10Code).IsRequired();
        });
    }
}