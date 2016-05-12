using System;
using NServiceBus;

namespace Messages
{
    public class SendCustomerDataEvent : IEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}