using Catalog.API.Entities;

namespace  Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProductById(string productId);
    }
}