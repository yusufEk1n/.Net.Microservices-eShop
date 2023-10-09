using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();

            return Ok(products);
        }
        
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string productId)
        {
            var product = await _repository.GetProductById(productId);

            if(product == null)
            {
                _logger.LogError($"Product with id: {productId}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{productName}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string productName)
        {
            var products = await _repository.GetProductByName(productName);

            if(products == null)
            {
                _logger.LogError($"Product with name: {productName}, not found.");
                return NotFound();
            }

            return Ok(products);
        }

        [Route("[action]/{categoryName}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string categoryName)
        {
            var products = await _repository.GetProductByCategory(categoryName);

            if(products == null)
            {
                _logger.LogError($"Product with category: {categoryName}, not found.");
                return NotFound();
            }

            return Ok(products);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);

            //invoke GetProductById method with id parameter.
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] 
        public async Task<ActionResult> DeleteProductById(string productId)
        {
            return Ok(await _repository.DeleteProductById(productId));
        }
    }
}