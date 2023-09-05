using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreConfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbcontextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>
            {
                new Order() { UserName = "kmn", FirstName = "Yusuf", LastName = "Ekin", EmailAddress = "yusuf@gmail.com", AddressLine = "Istanbul", Country = "Turkey", State = "Istanbul", Zipcode = "34000", CardNumber = "1234567891234567", Expiration = "12/22", CVV = "123", PaymentMethod = 1, CardName = "X", TotalPrice = 350 }
            };
        }
    }
}
