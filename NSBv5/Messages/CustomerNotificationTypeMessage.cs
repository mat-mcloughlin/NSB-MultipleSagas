using System;
using NServiceBus;

namespace Messages
{
    public class CustomerNotificationTypeMessage : IMessage
    {
        public DispatchMethod DispatchMethod { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}