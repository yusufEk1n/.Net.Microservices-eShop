using ClientApp.Extensions;
using ClientApp.Models;

namespace ClientApp.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _httpClient.GetAsync("/Catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _httpClient.GetAsync($"/Catalog/{id}?productId={id}");
            return await response.ReadContentAs<CatalogModel>();
        }
        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _httpClient.GetAsync($"/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var response = await _httpClient.PostAsJson("/Catalog", model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CatalogModel>();
            else
                throw new ApplicationException("Something went wrong when calling API");
        }
    }
}
