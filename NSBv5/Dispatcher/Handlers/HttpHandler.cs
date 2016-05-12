using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Dispatcher.Handlers
{
    public class HttpHandler : IHandleMessages<SendCustomerDataEvent>
    {
        static ILog log = LogManager.GetLogger<HttpHandler>();

        public void Handle(SendCustomerDataEvent message)
        {
            if (message.DeliveryMethod == DeliveryMethod.Http)
            {
                log.InfoFormat("Sending via HTTP; Customer: {0}, Product: {1}", message.CustomerId, message.ProductId);
            }
        }
    }
}
