using System;
using System.Collections.Generic;
using EntityFrameWorkDatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkDatabaseFirst.Data;

public partial class DemoCodeFirstContext : DbContext
{
    public DemoCodeFirstContext()
    {
    }

    public DemoCodeFirstContext(DbContextOptions<DemoCodeFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=DemoCodeFirst;integrated security=SSPI;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.CustomerId, "IX_Order_CustomerID");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.ToTable("ProductOrder");

            entity.HasIndex(e => e.OrderId, "IX_ProductOrder_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_ProductOrder_ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductOrders).HasForeignKey(d => d.ProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
