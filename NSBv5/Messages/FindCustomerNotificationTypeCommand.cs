using System;
using NServiceBus;

namespace Messages
{
    public class FindCustomerNotificationTypeCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}