using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class LocalDatabaseContext : DbContext
{

    protected LocalDatabaseContext()
    {
    }

    public LocalDatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<CustomerInfoEntity> CustomerInfo { get; set; }
    public virtual DbSet<CustomerAddressEntity> CustomerAddress { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<AddressEntity>()
            .HasIndex(x => x.Id)
            .IsUnique();

        modelBuilder.Entity<CustomerAddressEntity>()
            .HasKey(x => new { x.CustomerId, x.AddressId });
    }
}
