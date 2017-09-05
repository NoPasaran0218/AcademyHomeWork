
using MatOrderingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Storage
{
    public class OrdersDbContext:DbContext, IOrdersDbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapOrders(modelBuilder.Entity<Order>());
            MapOrders(modelBuilder.Entity<Product>());
            MapOrders(modelBuilder.Entity<OrderItem>());
        }


        protected void MapOrders(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.New);
            builder.Property(p => p.CreatorId)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.OrderDetails)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired()
                .ForSqlServerHasDefaultValueSql("GETDATE()");
            builder.Property(p => p.OrderCode)
                .IsRequired()
                .HasMaxLength(13);
            builder.HasIndex(p => p.OrderCode)
                .IsUnique();
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .HasPrincipalKey(p => p.Id);
        }

        protected void MapOrders(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasKey(p => p.Id);
        }

        protected void MapOrders(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.OrderId)
                .IsRequired();
            builder.Property(p => p.ProductId)
                .IsRequired();
            builder.Property(p => p.Count)
                .IsRequired();
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(p => p.OrderId)
                .HasPrincipalKey(p => p.Id);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .HasPrincipalKey(p => p.Id);
                
        }
    }
}
