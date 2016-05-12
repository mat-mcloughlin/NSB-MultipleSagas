using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Dispatcher2.Handlers
{
    public class FtpHandler : IHandleMessages<SendCustomerDataEvent>
    {
        static ILog log = LogManager.GetLogger<FtpHandler>();

        public void Handle(SendCustomerDataEvent message)
        {
            if (message.DeliveryMethod == DeliveryMethod.Ftp)
            {
                log.InfoFormat("Sending via FTP; Customer: {0}, Product: {1}", message.CustomerId, message.ProductId);
            }
        }
    }
}
