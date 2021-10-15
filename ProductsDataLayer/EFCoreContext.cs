using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System;

namespace ProductsDataLayer
{
    public class EFCoreContext : DbContext
    {
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryDb>().ToTable("Categories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
            });
        }
    }
}
