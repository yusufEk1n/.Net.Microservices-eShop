using Discount.API.Entities;

namespace Discount.API.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        /// <summary>
        /// Get discount by product name
        /// </summary>
        /// <param name="productName">product name argument</param>
        /// <returns>The <see cref="Coupon"/>.</returns>
        Task<Coupon> GetDiscount(string productName);

        /// <summary>
        /// Create discount
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task<bool> CreateDiscount(Coupon coupon);

        /// <summary>
        /// Update discount
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task<bool> UpdateDiscount(Coupon coupon);

        /// <summary>
        /// Delete discount
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        Task<bool> DeleteDiscount(string productName);
    }
}
