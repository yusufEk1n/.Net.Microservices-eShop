﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrderByUserNameAsync(string userName)
        {
            var orderList = await _orderContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();

            return orderList;
        }
    }
}
