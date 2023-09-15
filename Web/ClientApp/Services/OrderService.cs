using ClientApp.Extensions;
using ClientApp.Models;

namespace ClientApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _httpClient.GetAsync($"/order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
