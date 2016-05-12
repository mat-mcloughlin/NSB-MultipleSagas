using System;
using NServiceBus;

namespace Messages
{
    public class NotifyCustomerCommand : ICommand
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}