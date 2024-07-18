using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CustomerDetailsEntity> CustomerDetails { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CartEntity>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Carts)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            // Adding unique constraint on BookId and UserId combination
            modelBuilder.Entity<CartEntity>()
                .HasIndex(c => new { c.BookId, c.UserId })
                .IsUnique();

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.User)
                .WithMany(o => o.Order)
                .HasForeignKey(o => o.UserId);
                
        }
    }
}
