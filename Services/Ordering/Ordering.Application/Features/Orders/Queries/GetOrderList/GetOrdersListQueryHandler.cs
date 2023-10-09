using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    /// <summary>
    /// Get order list query handler class used to handle the request
    /// </summary>
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="request">Get order list query</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns> The <see cref="List{OrdersVm}"/> </returns>
        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserNameAsync(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orders);
        }
    }
}
