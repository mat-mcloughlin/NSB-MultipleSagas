using System.Linq;
using Messages;
using NServiceBus;

namespace Host.Handlers
{
    public class FindCustomerNotificationTypeHandler : IHandleMessages<FindCustomerNotificationTypeCommand>
    {
        private readonly IBus _bus;

        public FindCustomerNotificationTypeHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(FindCustomerNotificationTypeCommand message)
        {
            var customer = Customers.List.FirstOrDefault(c => c.Id == message.CustomerId);

            if (customer != null)
            {
                _bus.Reply(new CustomerNotificationTypeMessage
                {
                    CustomerId = message.CustomerId,
                    ProductId = message.ProductId,
                    DispatchMethod = customer.DispatchMethod,
                    DeliveryMethod = customer.DeliveryMethod
                });
            }
            else
            {
                var test = 1;
            }
        }
    }
}
