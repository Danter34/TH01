﻿using Microsoft.EntityFrameworkCore;
using TH01.Data;
using TH01.Models.Interface;

namespace TH01.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CoffeeshopDbContext dbContext;

        public ShoppingCartRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ShopingcartItem>? shopingcartItems { get; set; }
        public string? ShoppingCartId { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            CoffeeshopDbContext context = services.GetService<CoffeeshopDbContext>()
                ?? throw new Exception("Error initializing CoffeeshopDbContext");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s =>
                s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShopingcartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }

            dbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId);

            dbContext.ShoppingCartItems.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }

        public List<ShopingcartItem> GetAllShoppingCartItems()
        {
            return shopingcartItems ??= dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Product.Price * s.Qty)
                .Sum();

            return totalCost;
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(s =>
                s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var quantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }
    }
}