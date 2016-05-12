using System;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;

namespace Host.Sagas
{
    class CustomerNotificationSaga : Saga<CustomerNotificationData>,
        IAmStartedByMessages<NotifyCustomerCommand>,
        IHandleMessages<CustomerNotificationTypeMessage>,
        IHandleTimeouts<NightlyBatchTimeout>
    {
        static ILog log = LogManager.GetLogger<CustomerNotificationSaga>();

        public CustomerNotificationSaga()
        {
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CustomerNotificationData> mapper)
        {
            mapper.ConfigureMapping<NotifyCustomerCommand>(msg => msg.CustomerId).ToSaga(sagadata => sagadata.CustomerId);
        }

        public void Handle(NotifyCustomerCommand message)
        {
            Data.CustomerId = message.CustomerId;

            // At this point we attempt to get the notification type if we don't have it. If we do we just carrry on
            // We could set up another handler method that could handle adjusting of the notification type on the fly.
            if (Data.DispatchMethod == null)
            {
                Bus.SendLocal(new FindCustomerNotificationTypeCommand() { CustomerId = message.CustomerId, ProductId = message.ProductId });
                //RequestTimeout<NightlyBatchTimeout>(DateTime.UtcNow.Date.AddDays(1));
                RequestTimeout<NightlyBatchTimeout>(TimeSpan.FromSeconds(10));
            }
            else
            {
                HandlerNotification(message.ProductId);
            }
        }

        public void Handle(CustomerNotificationTypeMessage message)
        {
            Data.DispatchMethod = message.DispatchMethod;
            Data.DeliveryMethod = message.DeliveryMethod;
            HandlerNotification(message.ProductId);
        }

        void HandlerNotification(Guid productId)
        {
            switch (Data.DispatchMethod)
            {
                case DispatchMethod.Immediately:
                    log.InfoFormat("Sending immediately; Customer: {0}, Product: {1}", Data.CustomerId, productId);
                    Bus.Publish(new SendCustomerDataEvent() { CustomerId = Data.CustomerId, ProductId = productId, DeliveryMethod = Data.DeliveryMethod });
                    break;
                case DispatchMethod.NigthlyBatch:
                    log.InfoFormat("Saving for later; Customer: {0}, Product: {1}", Data.CustomerId, productId);
                    Data.GatheredProducts.Add(new Product { GatheredProductId = productId });
                    break;
            }
        }

        public void Timeout(NightlyBatchTimeout state)
        {
            foreach (var gatheredProduct in Data.GatheredProducts)
            {
                log.InfoFormat("Send as batch; Customer: {0}, Product: {1}", Data.CustomerId, gatheredProduct.GatheredProductId);
                Bus.Publish(new SendCustomerDataEvent() { CustomerId = Data.CustomerId, ProductId = gatheredProduct.GatheredProductId, DeliveryMethod = Data.DeliveryMethod });
            }

            log.InfoFormat("Closing Saga; Customer: {0}", Data.CustomerId);
            MarkAsComplete();
        }
    }
}
