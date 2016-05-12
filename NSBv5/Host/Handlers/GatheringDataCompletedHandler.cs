using System;
using System.Collections.Generic;
using System.Linq;
using Messages;
using NServiceBus;

namespace Host.Handlers
{
    public class GatheringDataCompletedHandler : IHandleMessages<GatheringDataCompletedEvent>
    {
        readonly IBus _bus;

        public GatheringDataCompletedHandler(IBus bus)
        {
            _bus = bus;
        }
        public void Handle(GatheringDataCompletedEvent message)
        {
            foreach (var customer in GetCustomers(message.ProductId))
            {
                _bus.SendLocal(new NotifyCustomerCommand() { ProductId = message.ProductId, CustomerId = customer });
            }
        }

        private IEnumerable<Guid> GetCustomers(Guid productId)
        {
            // We would retrieve a list of customers that would be interested in this product
            var rand = new Random();
            var numberOfCustomers = rand.Next(0, 20);
            return Customers.List.Take(numberOfCustomers).Select(c => c.Id);
        }
    }
}