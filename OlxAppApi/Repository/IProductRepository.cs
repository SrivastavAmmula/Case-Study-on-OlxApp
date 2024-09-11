using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<List<Product>> GetProductsByCategoryIdAsync(string categoryId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(string id);
        Task<List<Product>> GetFilteredProductsAsync(string location, string name, decimal? minPrice, decimal? maxPrice);


    }
}
