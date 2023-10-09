using Basket.API.Entities;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        /// <summary>
        /// Get the basket with <see cref="IDistributedCache"/> instance.
        /// Then, deserilaze the string to <see cref="ShoppingCart"/> object.
        /// </summary>
        /// <param name="userName">user name argument</param>
        /// <returns>The <see cref="ShoppingCart"/>.</returns>
        Task<ShoppingCart> GetBasket(string userName);

        /// <summary>
        /// Serilaze the <see cref="ShoppingCart"/> object to string.
        /// Then, updated the basket with <see cref="IDistributedCache"/> instance.
        /// </summary>
        /// <param name="basket">The <see cref="ShoppingCart"/> object.</param>
        /// <returns>The <see cref="ShoppingCart"/>.</returns>
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

        /// <summary>
        /// Delete the basket with <see cref="IDistributedCache"/> instance.
        /// </summary>
        /// <param name="userName">user name argument</param>
        Task DeleteBasket(string userName);
    }
}