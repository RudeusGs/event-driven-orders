using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MessageLog> MessageLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.TotalAmount)
                      .HasColumnType("numeric(18,2)");

                entity.Property(x => x.Status)
                      .IsRequired();

                entity.HasMany(o => o.Items)
                      .WithOne()
                      .HasForeignKey(oi => oi.OrderId);
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Price)
                      .HasColumnType("numeric(18,2)");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Price)
                      .HasColumnType("numeric(18,2)");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                      .IsRequired();
            });
            modelBuilder.Entity<MessageLog>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Payload)
                      .HasColumnType("text");
            });
        }
    }
}