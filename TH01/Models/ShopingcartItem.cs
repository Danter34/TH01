namespace TH01.Models
{
    public class ShopingcartItem
    {
        public int Id { get; set; }

        public Product? Product { get; set; }

        public int Qty { get; set; }

        public string? ShoppingCartId { get; set; }
    }
}
