﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository,
                                           IMapper mapper, 
                                           IEmailService emailService,
                                           ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = await _orderRepository.AddAsync(_mapper.Map<Order>(request));

            _logger.LogInformation($"Order {newOrder.Id} is succesfully created.");
            await SendMailAsync(newOrder);
            return newOrder.Id;
        }

        private async Task SendMailAsync(Order order)
        {
            var email = new Email()
            {
                To = "yusufekin@gmail.com",
                Body = "Order was created.",
                Subject = "Order was created."
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
                await _orderRepository.DeleteAsync(order);
            }
        }
    }
}
