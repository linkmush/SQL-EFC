using System;
using System.Collections.Generic;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\SQL-EFC\\Infrastructure\\Data\\local_ProductCatalog_dbf.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07056E9D48");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Currenci__A25C5AA6EA2319AF");

            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07DF888358");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C9911435047C278");

            entity.Property(e => e.ArticleNumber).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__3E52440B");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__3D5E1FD2");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.Property(e => e.CurrencyCode).IsFixedLength();

            entity.HasOne(d => d.ArticleNumberNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Artic__403A8C7D");

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Curre__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
