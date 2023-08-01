using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Product> GetProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, productId);

            return await _context
                            .Products
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, productName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Products
                            .Find(p => p.Category == categoryName)
                            .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context
                    .Products
                    .InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateProduct = await _context
                                        .Products
                                        .ReplaceOneAsync(p => p.Id == product.Id, product);

            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            
            var deleteProduct = await _context
                                        .Products
                                        .DeleteOneAsync(filter);

            return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount > 0;
        }
    }
}