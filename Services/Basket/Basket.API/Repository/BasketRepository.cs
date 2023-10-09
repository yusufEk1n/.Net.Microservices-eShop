using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    /// <summary>
    /// The Basket Repository used for interacting with the Redis Database
    /// </summary>
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache disturbutedCache)
        {
            _redisCache = disturbutedCache ?? throw new ArgumentNullException(nameof(disturbutedCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            //string basket deserilaze to ShoppingCart object
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            //ShoppingCart object serilaze to string 
            var basketJson = JsonConvert.SerializeObject(basket);

            await _redisCache.SetStringAsync(basket.UserName, basketJson);

            return await GetBasket(basket.UserName);
        }
       
        public async Task DeleteBasket(string userName) => await _redisCache.RemoveAsync(userName);
    }
}