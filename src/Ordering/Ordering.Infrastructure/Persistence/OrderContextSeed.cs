using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation($"Seed database with context");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders() =>
            new List<Order>
            {
                new Order
                {
                    UserName = "swn",
                    FirstName = "Rob",
                    LastName = "Dale",
                    EmailAddress = "rob@robdale.me.uk",
                    AddressLine1 = "1 The Road",
                    Country = "Turkey",
                    TotalPrice = 350
                }
            };

    }
}
