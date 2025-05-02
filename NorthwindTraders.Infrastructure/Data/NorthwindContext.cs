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
                entity.HasKey(o => o.OrderId);

                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Employee)
                    .WithMany(emp => emp.Orders)
                    .HasForeignKey(o => o.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(o => o.ShipName).HasMaxLength(40);
                entity.Property(o => o.ShipAddress).HasMaxLength(60);
                entity.Property(o => o.ShipCity).HasMaxLength(15);
                entity.Property(o => o.ShipRegion).HasMaxLength(15);
                entity.Property(o => o.ShipPostalCode).HasMaxLength(10);
                entity.Property(o => o.ShipCountry).HasMaxLength(15);
                entity.Property(o => o.Freight).HasColumnType("money");
                entity.Property(o => o.OrderDate).HasColumnType("datetime");
                entity.Property(o => o.RequiredDate).HasColumnType("datetime");
                entity.Property(o => o.ShippedDate).HasColumnType("datetime");
                entity.Property(o => o.ShipVia).HasColumnType("int");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => new { od.OrderId, od.ProductId });

                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetail)
                    .HasForeignKey(od => od.OrderId);

                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(od => od.ProductId);

                entity.ToTable("Order Details");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
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
