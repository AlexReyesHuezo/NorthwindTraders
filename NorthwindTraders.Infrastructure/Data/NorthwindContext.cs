using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthwindTraders.Domain.Entities;

namespace NorthwindTraders.Infrastructure.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasOne(e => e.Customer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    // I use Restrict here to prevent deleting a customer that has orders
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Employee)
                    .WithMany(emp => emp.Orders)
                    .HasForeignKey(e => e.EmployeeId);
                
                entity.Property(e => e.ShipName).HasMaxLength(40);
                entity.Property(e => e.ShipAddress).HasMaxLength(60);
                entity.Property(e => e.ShipCity).HasMaxLength(15);
                entity.Property(e => e.ShipRegion).HasMaxLength(15);
                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);
                entity.Property(e => e.ShipCountry).HasMaxLength(15);
                entity.Property(e => e.Freight).HasColumnType("money");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");
                entity.Property(e => e.ShipVia).HasColumnType("int");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => new { od.OrderId, od.ProductId });

                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetail)
                    .HasForeignKey(od  => od.OrderId);

                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(od => od.ProductId);

                entity.ToTable("Order Details");
            });

            modelBuilder.Entity<Employee>(entity => {
                entity.HasKey(e => e.EmployeeId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.CustomerId)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.HasKey(s => s.ShipperId);
            });
        }
    }
}
