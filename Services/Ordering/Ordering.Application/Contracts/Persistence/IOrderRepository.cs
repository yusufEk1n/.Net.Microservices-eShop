using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        /// <summary>
        /// Get orders by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns> The <see cref="IEnumerable{Order}"/></returns>
        Task<IEnumerable<Order>> GetOrderByUserNameAsync(string userName);
    }
}
