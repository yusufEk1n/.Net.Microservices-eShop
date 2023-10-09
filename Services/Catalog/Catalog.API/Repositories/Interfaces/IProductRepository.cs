using Catalog.API.Entities;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;

namespace  Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Get the products with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Product}"/> of products.</returns>
        Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Get product by id with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The <see cref="Product"/>.</returns>
        Task<Product> GetProductById(string productId);

        /// <summary>
        /// Get product by name with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="productName">The product name.</param>
        /// <returns>The <see cref="IEnumerable{Product}"/>.</returns>
        Task<IEnumerable<Product>> GetProductByName(string productName);

        /// <summary>
        /// Get product by category with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="categoryName">The product name.</param>
        /// <returns>The <see cref="IEnumerable{Product}"/>.</returns>
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        /// <summary>
        /// Create product with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="Product">The product name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task CreateProduct(Product product);

        /// <summary>
        /// Update product with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="Product">The product name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task<bool> UpdateProduct(Product product);

        /// <summary>
        /// Delete product with <see cref="ICatalogContext"/> instance.
        /// </summary>
        /// <param name="productId">The product name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task<bool> DeleteProductById(string productId);
    }
}