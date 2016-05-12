using System;
using System.Collections.Generic;
using Messages;
using NServiceBus.Saga;

namespace Host.Sagas
{
    public class CustomerNotificationData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual Guid CustomerId { get; set; }
        public virtual IList<Product> GatheredProducts { get; set; }
        public virtual DispatchMethod? DispatchMethod { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
    }

    public class Product
    {
        public virtual Guid GatheredProductId { get; set; }
    }
}