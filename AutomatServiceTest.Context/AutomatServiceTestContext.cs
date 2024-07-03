using AutomatServiceTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomatServiceTest.Context
{
    public class AutomatServiceTestContext : DbContext
    {
        public AutomatServiceTestContext()
        {

        }

        public AutomatServiceTestContext(DbContextOptions<AutomatServiceTestContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StorageProduct> StorageProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageProduct>()
                .HasKey(sp => new { sp.StorageId, sp.ProductId });

            modelBuilder.Entity<StorageProduct>()
                .HasIndex(sp => new { sp.StorageId, sp.ProductId })
                .IsUnique();
        }
    }
}
