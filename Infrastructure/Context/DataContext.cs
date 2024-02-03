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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0795BAD1D9");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Currenci__A25C5AA6FB1C1919");

            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07C1BEB65B");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C991143B4FD5D12");

            entity.Property(e => e.ArticleNumber).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__5629CD9C");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__5535A963");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__ProductP__3C991143E364E012");

            entity.Property(e => e.ArticleNumber).ValueGeneratedNever();
            entity.Property(e => e.CurrencyCode).IsFixedLength();

            entity.HasOne(d => d.ArticleNumberNavigation).WithOne(p => p.ProductPrice)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Artic__5DCAEF64");

            entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.ProductPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Curre__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
