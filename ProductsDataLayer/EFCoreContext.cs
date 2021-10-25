using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;

namespace ProductsDataLayer
{
    public class EFCoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<AccountInfo> Users { get; set; }

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(product => product.Id);
            });

            modelBuilder.Entity<AccountInfo>()
                .OwnsOne(accountInfo => accountInfo.LoginInfo,
                navigationBuilder =>
                {
                    navigationBuilder.Property(loginInfo => loginInfo.Login)
                        .HasColumnName("Login");
                    navigationBuilder.Property(loginInfo => loginInfo.Password)
                        .HasColumnName("Password");
                });
        }
    }
}
