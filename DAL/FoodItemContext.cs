using Microsoft.EntityFrameworkCore;
using epos.Models;

namespace epos.DAL
{
    public class FoodItemContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = shop.db");
        }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}