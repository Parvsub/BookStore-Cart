using BookStore_Cart.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStore_Cart.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartEntity>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
        }
        public DbSet<CartEntity> CartItems { get; set; }
    }
}
