using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TH01.Models;

namespace TH01.Data
{
    public class CoffeeshopDbContext: IdentityDbContext
    {
        public CoffeeshopDbContext(DbContextOptions<CoffeeshopDbContext> options) :base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopingcartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Contact> ContactMessages { get; set; }
        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
           
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "America",
                Price = 25,
                Detail = "Name product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 2,
                Name = "Vietnam",
                Price = 20,
                Detail = "Vietnamese product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 3,
                Name = "United Kingdom",
                Price = 15,
                Detail = "UK product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 4,
                Name = "India",
                Price = 15,
                Detail = "India product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 5,
                Name = "Russian",
                Price = 25,
                Detail = "Russian product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            },
            new Product
            {
                Id = 6,
                Name = "France",
                Price = 35,
                Detail = "France product",
                ImageUrl =
            "https://insanelygoodrecipes.com/wp-content/uploads/2020/07/Cup-Of-Creamy-Coffee-1024x536.webp"
            }
            );
        }
    }
}
