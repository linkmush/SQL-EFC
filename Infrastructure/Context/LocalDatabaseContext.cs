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

        modelBuilder.Entity<CustomerAddressEntity>()
            .HasKey(x => new { x.CustomerId, x.AddressId });

        modelBuilder.Entity<AddressEntity>()
            .HasMany(a => a.CustomerAddress)
            .WithOne(ca => ca.Address)
            .HasForeignKey(ca => ca.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CustomerEntity>()
            .HasMany(c => c.CustomerAddress)
            .WithOne(ca => ca.Customer)
            .HasForeignKey(ca => ca.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
