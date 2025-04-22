namespace TH01.Models.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetTrendingProducts();
        Product GetProductDetail(int id);
    }
}
